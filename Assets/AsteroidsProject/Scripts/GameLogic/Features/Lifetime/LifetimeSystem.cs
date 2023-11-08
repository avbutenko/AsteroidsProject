using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Lifetime
{
    public class LifetimeSystem : IEcsRunSystem
    {
        private readonly ITimeService timeService;

        public LifetimeSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CLifetime>().End();
            var lifetimePool = world.GetPool<CLifetime>();

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