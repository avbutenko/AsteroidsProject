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
            var filter = world.Filter<Lifetime>().End();

            var lifetimePool = world.GetPool<Lifetime>();
            //var destroyTagPool = world.GetPool<CDestructionRequest>();

            foreach (var entity in filter)
            {
                ref var lifetime = ref lifetimePool.Get(entity).Value;
                lifetime -= timeService.DeltaTime;

                if (lifetime <= 0)
                {
                    //destroyTagPool.Add(entity);
                }
            }
        }
    }
}
