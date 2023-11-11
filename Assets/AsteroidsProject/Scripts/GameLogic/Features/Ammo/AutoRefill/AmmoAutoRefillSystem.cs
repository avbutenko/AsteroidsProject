using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Ammo.AutoRefill
{
    public class AmmoAutoRefillSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CAmmoAutoRefill> paramsPool;
        private EcsPool<CAmmoAutoRefillCoolDown> coolDownPool;
        private EcsPool<CChangeAmmoAmountRequest> requestPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            filter = world.Filter<CAmmoAutoRefill>()
                          .Exc<CChangeAmmoAmountRequest>()
                          .Exc<CAmmoAutoRefillCoolDown>()
                          .End();

            paramsPool = world.GetPool<CAmmoAutoRefill>();
            coolDownPool = world.GetPool<CAmmoAutoRefillCoolDown>();
            requestPool = world.GetPool<CChangeAmmoAmountRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var timer = ref paramsPool.Get(entity).Timer;
                ref var amount = ref paramsPool.Get(entity).Amount;

                requestPool.Add(entity).Value = amount;
                coolDownPool.Add(entity).Value = timer;
            }
        }
    }
}