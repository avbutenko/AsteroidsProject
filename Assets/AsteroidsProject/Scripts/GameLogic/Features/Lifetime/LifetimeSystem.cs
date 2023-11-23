using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Lifetime
{
    public class LifetimeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ITimeService timeService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CLifetime> lifetimePool;

        public LifetimeSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CLifetime>().End();
            lifetimePool = world.GetPool<CLifetime>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var lifetime = ref lifetimePool.Get(entity).Value;
                lifetime -= timeService.DeltaTime;

                if (lifetime <= 0)
                {
                    world.NewEntityWith(new CDeathEvent { PackedEntity = world.PackEntity(entity) });
                }
            }
        }
    }
}