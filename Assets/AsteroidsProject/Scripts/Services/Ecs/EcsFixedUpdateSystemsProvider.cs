using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AsteroidsProject.Services
{
    public class EcsFixedUpdateSystemsProvider : IEcsFixedUpdateSystemsProvider, IDisposable
    {
        private readonly List<IEcsSystem> bindedSystems;

        public EcsFixedUpdateSystemsProvider(IEnumerable<IEcsSystem> bindedSystems)
        {
            this.bindedSystems = bindedSystems.ToList();
        }

        public List<IEcsSystem> BindedSystems => bindedSystems;

        public void Dispose()
        {
            bindedSystems.Clear();
        }
    }
}