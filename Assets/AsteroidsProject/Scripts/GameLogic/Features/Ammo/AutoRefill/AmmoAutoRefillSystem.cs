using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Ammo
{
    public class AmmoAutoRefillSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CAmmoAutoRefill> paramsPool;
        private EcsPool<CAmmoAutoRefillRequest> ammoRefillRequestPool;
        private EcsPool<CChangeAmmoAmountRequest> changeAmmoRequestPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            filter = world.Filter<CAmmoAutoRefill>()
                          .Inc<CAmmoAutoRefillRequest>()
                          .End();

            paramsPool = world.GetPool<CAmmoAutoRefill>();
            ammoRefillRequestPool = world.GetPool<CAmmoAutoRefillRequest>();
            changeAmmoRequestPool = world.GetPool<CChangeAmmoAmountRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var amount = ref paramsPool.Get(entity).Amount;
                changeAmmoRequestPool.Add(entity).Value = amount;
                ammoRefillRequestPool.Del(entity);
            }
        }
    }
}