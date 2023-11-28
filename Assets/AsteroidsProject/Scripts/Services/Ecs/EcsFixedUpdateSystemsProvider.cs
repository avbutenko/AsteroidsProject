using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using System.Collections.Generic;

namespace AsteroidsProject.Services
{
    public class EcsFixedUpdateSystemsProvider : IEcsFixedUpdateSystemsProvider
    {
        private readonly IEnumerable<IEcsSystem> bindedSystems;

        public EcsFixedUpdateSystemsProvider(IEnumerable<IEcsSystem> bindedSystems)
        {
            this.bindedSystems = bindedSystems;
        }

        public IEnumerable<IEcsSystem> BindedSystems => bindedSystems;
    }
}