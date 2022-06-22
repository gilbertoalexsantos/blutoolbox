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
        public abstract Type AnyTypeFromGameAssembly { get; }
        
        public virtual void InstallBindings(DiContainer container)
        {
            BindClassInstallers(container);
        }

        private void BindClassInstallers(DiContainer container)
        {
            Type classInstallerType = typeof(ClassInstaller);
            IEnumerable<Type> customClassTypes = Assembly.GetAssembly(AnyTypeFromGameAssembly).GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(classInstallerType));
            foreach (Type type in customClassTypes)
            {
                ClassInstaller installer = (ClassInstaller) Activator.CreateInstance(type);
                installer.InstallBindings(container);
            }
        }
    }
}