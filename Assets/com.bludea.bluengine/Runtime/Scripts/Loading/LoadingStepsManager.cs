using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bludk;
using Zenject;

namespace BluEngine
{
    public class LoadingStepsManager
    {
        private class Node
        {
            public int InCount;
            public LoadingStep Step;
            public List<Node> DependsOnMe = new();
        }

        private readonly DiContainer _injector;
        private List<LoadingStep> _steps;
        private bool _initialized;

        public LoadingStepsManager(DiContainer injector)
        {
            _injector = injector;
        }

        public void Init(IEnumerable<LoadingStep> steps)
        {
            if (_initialized)
            {
                throw new Exception("LoadingStepsManager already initialized");
            }

            if (steps == null)
            {
                throw new ArgumentException($"parameter {nameof(steps)} is null");
            }

            _initialized = true;
            _steps = new List<LoadingStep>(steps);
        }

        public IEnumerator Execute()
        {
            if (!_initialized)
            {
                throw new Exception("LoadingStepsManager is not initialized");
            }

            List<List<LoadingStep>> steps = LoadStepsInExecutionOrder();
            if (steps.Count == 0)
            {
                return null;
            }

            IEnumerator head = TxongaHelper.Empty;
            for (int i = 0; i < steps.Count; i++)
            {
                int idx = i;
                head = head.ThenAll(() => TxongaHelper.All(steps[idx].Select(step =>
                {
                    _injector.Inject(step);
                    return step.Execute();
                })));
            }

            return head;
        }

        private List<List<LoadingStep>> LoadStepsInExecutionOrder()
        {
            if (_steps.Count == 0)
            {
                return new List<List<LoadingStep>>();
            }

            Dictionary<LoadingStep, int> stepsIdx = new();
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < _steps.Count; i++)
            {
                nodes.Add(new Node {Step = _steps[i]});
                stepsIdx[_steps[i]] = i;
            }

            foreach (Node u in nodes)
            {
                foreach (LoadingStep dependencyStep in u.Step.Dependencies)
                {
                    Node v = nodes[stepsIdx[dependencyStep]];
                    v.DependsOnMe.Add(u);
                    u.InCount++;
                }
            }

            Queue<Node> q = new();
            nodes.ForEach(node =>
            {
                if (node.InCount == 0)
                {
                    q.Enqueue(node);
                }
            });
            if (q.Count == 0)
            {
                throw new Exception("At least one step should not have dependencies");
            }

            List<List<LoadingStep>> result = new();
            HashSet<LoadingStep> processedSteps = new();
            while (q.Count > 0)
            {
                List<Node> actualNodes = new();
                while (q.Count > 0)
                {
                    Node node = q.Dequeue();
                    if (processedSteps.Contains(node.Step))
                    {
                        throw new Exception($"Loop on dependencies. Step {node.Step.name}");
                    }

                    processedSteps.Add(node.Step);
                    actualNodes.Add(node);
                }

                result.Add(actualNodes.Select(node => node.Step).ToList());

                List<Node> nextModes = new();
                foreach (Node u in actualNodes)
                {
                    foreach (Node v in u.DependsOnMe)
                    {
                        v.InCount--;
                        if (v.InCount == 0)
                        {
                            nextModes.Add(v);
                        }
                    }
                }

                foreach (Node node in nextModes)
                {
                    q.Enqueue(node);
                }
            }

            return result;
        }
    }
}