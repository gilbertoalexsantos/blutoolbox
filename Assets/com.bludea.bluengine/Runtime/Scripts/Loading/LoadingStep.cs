using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BluEngine
{
    public abstract class LoadingStep : ScriptableObject
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        private List<LoadingStep> _dependencies;

        public List<LoadingStep> Dependencies => _dependencies;

        public string Name => _name;

        public abstract IEnumerator Execute();
    }
}