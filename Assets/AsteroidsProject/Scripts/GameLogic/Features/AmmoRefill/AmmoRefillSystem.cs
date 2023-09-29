using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.AmmoRefill
{
    public class AmmoRefillSystem : IEcsRunSystem
    {
        private readonly ITimeService timeService;

        public AmmoRefillSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<AmmoRefillCoolDown>().End();

            var ammoRefillCoolDownPool = world.GetPool<AmmoRefillCoolDown>();
            var activeAmmoRefillCoolDownPool = world.GetPool<ActiveAmmoRefillCoolDown>();
            var ammoPool = world.GetPool<Ammo>();

            foreach (var entity in filter)
            {
                if (!activeAmmoRefillCoolDownPool.Has(entity))
                {
                    activeAmmoRefillCoolDownPool.Add(entity);
                }

                ref var ammoRefillCoolDown = ref ammoRefillCoolDownPool.Get(entity).Value;
                ref var activeAmmoRefillCoolDown = ref activeAmmoRefillCoolDownPool.Get(entity).Value;

                activeAmmoRefillCoolDown += timeService.DeltaTime;

                if (activeAmmoRefillCoolDown >= ammoRefillCoolDown)
                {
                    activeAmmoRefillCoolDown = 0f;

                    if (ammoPool.Has(entity))
                    {
                        ammoPool.Get(entity).Value++;
                    }
                    else
                    {
                        ammoPool.Add(entity).Value++;
                    }
                }
            }
        }
    }
}
