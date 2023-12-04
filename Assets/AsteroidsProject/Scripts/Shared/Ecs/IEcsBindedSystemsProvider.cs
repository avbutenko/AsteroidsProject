using Leopotam.EcsLite;
using System.Collections.Generic;

namespace AsteroidsProject.Shared
{
    public interface IEcsBindedSystemsProvider
    {
        public List<IEcsSystem> BindedSystems { get; }
    }
}