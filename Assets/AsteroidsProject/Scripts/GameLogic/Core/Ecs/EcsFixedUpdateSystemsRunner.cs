using LeoEcsPhysics;
using Leopotam.EcsLite;
using System.Collections.Generic;
using Zenject;

namespace AsteroidsProject.GameLogic.Core
{
    public sealed class EcsFixedUpdateSystemsRunner : EcsBaseStartup, IFixedTickable
    {
        private readonly EcsWorld world;
        public EcsFixedUpdateSystemsRunner(EcsWorld world, IEnumerable<IEcsSystem> bindedSystems) : base(world, bindedSystems)
        {
            this.world = world;
        }

        public override void Initialize()
        {
            EcsPhysicsEvents.ecsWorld = world;
            base.Initialize();
        }

        public void FixedTick()
        {
            systems?.Run();
        }

        public override void Dispose()
        {
            EcsPhysicsEvents.ecsWorld = null;
            base.Dispose();
        }
    }
}