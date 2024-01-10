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

        public IEcsSystem Create(Type type)
        {
            return (IEcsSystem)diContainer.Instantiate(type);
        }
    }
}