using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Zenject;

namespace BluEngine
{
    public abstract class AppStartCustomScriptableInstaller : ScriptableObject
    {
        protected virtual IEnumerable<Assembly> CustomInstallerAssemblies => Enumerable.Empty<Assembly>();

        public virtual void InstallBindings(DiContainer container)
        {
            BindClassInstallers(container);
        }

        private void BindClassInstallers(DiContainer container)
        {
            Type classInstallerType = typeof(ClassInstaller);
            foreach (Assembly assembly in CustomInstallerAssemblies)
            {
                IEnumerable<Type> customClassTypes = assembly.GetTypes()
                    .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(classInstallerType));
                foreach (Type type in customClassTypes)
                {
                    ClassInstaller installer = (ClassInstaller) Activator.CreateInstance(type);
                    installer.InstallBindings(container);
                }   
            }
        }
    }
}