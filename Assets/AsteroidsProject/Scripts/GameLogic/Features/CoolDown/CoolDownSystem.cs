using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.CoolDown
{
    public class CoolDownSystem : IEcsRunSystem
    {
        private readonly ITimeService timeService;

        public CoolDownSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CActiveCoolDown>().End();

            var activeCoolDownPool = world.GetPool<CActiveCoolDown>();

            foreach (var entity in filter)
            {
                ref var activeCoolDown = ref activeCoolDownPool.Get(entity).Value;

                activeCoolDown -= timeService.DeltaTime;

                if (activeCoolDown <= 0)
                {
                    activeCoolDownPool.Del(entity);
                }
            }
        }
    }
}