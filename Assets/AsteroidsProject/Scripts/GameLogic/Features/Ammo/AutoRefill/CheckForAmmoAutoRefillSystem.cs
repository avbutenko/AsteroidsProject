using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Ammo
{
    public class CheckForAmmoAutoRefillSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CAmmoAutoRefill> paramsPool;
        private EcsPool<CAmmoAutoRefillCoolDown> coolDownPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            filter = world.Filter<CAmmoAutoRefill>()
                          .Inc<CChangeAmmoAmountRequest>()
                          .Exc<CAmmoAutoRefillCoolDown>()
                          .End();

            paramsPool = world.GetPool<CAmmoAutoRefill>();
            coolDownPool = world.GetPool<CAmmoAutoRefillCoolDown>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var timer = ref paramsPool.Get(entity).Timer;
                coolDownPool.Add(entity).Value = timer;
            }
        }
    }
}