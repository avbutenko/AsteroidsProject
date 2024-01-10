using Leopotam.EcsLite;
using System;

namespace AsteroidsProject.Shared
{
    public interface IEcsSystemsFactory
    {
        public IEcsSystem Create(Type type);
    }
}