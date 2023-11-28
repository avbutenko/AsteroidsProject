using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using System.Collections.Generic;

namespace AsteroidsProject.Services
{
    public class EcsUpdateSystemsProvider : IEcsUpdateSystemsProvider
    {
        private readonly IEnumerable<IEcsSystem> bindedSystems;

        public EcsUpdateSystemsProvider(IEnumerable<IEcsSystem> bindedSystems)
        {
            this.bindedSystems = bindedSystems;
        }

        public IEnumerable<IEcsSystem> BindedSystems => bindedSystems;
    }
}