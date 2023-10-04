using Leopotam.EcsLite;
using System.Collections.Generic;
using Zenject;

namespace AsteroidsProject.GameLogic.Core
{
    public sealed class EcsUpdateSystemsRunner : EcsBaseStartup, ITickable
    {
        public EcsUpdateSystemsRunner(EcsWorld world, IEnumerable<IEcsSystem> bindedSystems) : base(world, bindedSystems) { }

        public void Tick()
        {
            systems?.Run();
        }
    }
}