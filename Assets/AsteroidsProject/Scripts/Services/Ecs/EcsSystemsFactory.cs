using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using System;
using Zenject;

namespace AsteroidsProject.Services
{
    public class EcsSystemsFactory : IEcsSystemsFactory
    {
        private readonly DiContainer diContainer;

        public EcsSystemsFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public IEcsSystem Create(string typeName)
        {
            var type = GetType(typeName);
            return (IEcsSystem)diContainer.Instantiate(type);
        }

        private Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                    return type;
            }
            return null;
        }
    }
}