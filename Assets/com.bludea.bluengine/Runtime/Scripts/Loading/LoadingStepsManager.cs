using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bludk;
using UnityEngine;
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

        public LoadingStepsManager(DiContainer injector)
        {
            _injector = injector;
        }

        public IEnumerator Execute()
        {
            List<List<LoadingStep>> steps = LoadStepsInExecutionOrder();
            if (steps.Count == 0)
            {
                return null;
            }

            IEnumerator head = TxongaHelper.Empty();
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
            List<LoadingStep> steps = LoadSteps();
            if (steps.Count == 0)
            {
                return new List<List<LoadingStep>>();
            }

            Dictionary<LoadingStep, int> stepsIdx = new();
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < steps.Count; i++)
            {
                nodes.Add(new Node {Step = steps[i]});
                stepsIdx[steps[i]] = i;
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

        private List<LoadingStep> LoadSteps()
        {
            GameSettings gameSettings = Resources.Load<GameSettings>(GameSettings.ResourcesPath);

            List<LoadingStep> loadingSteps = new List<LoadingStep>();
            foreach (string resourcesFolder in gameSettings.LoadingStepsResourcesFolders)
            {
                loadingSteps.AddRange(Resources.LoadAll<LoadingStep>(resourcesFolder));
            }

            return loadingSteps;
        }
    }
}