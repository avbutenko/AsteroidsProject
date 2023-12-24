using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Ammo
{
    public class AmmoMaxSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CAmmoMax> ammoMaxPool;
        private EcsPool<CAmmo> ammoPool;
        private EcsPool<CChangeAmmoAmountRequest> requestPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            filter = world.Filter<CAmmoMax>()
                          .Inc<CAmmo>()
                          .Inc<CChangeAmmoAmountRequest>()
                          .End();

            ammoMaxPool = world.GetPool<CAmmoMax>();
            ammoPool = world.GetPool<CAmmo>();
            requestPool = world.GetPool<CChangeAmmoAmountRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var currentValue = ref ammoPool.Get(entity).Value;
                ref var deltaValue = ref requestPool.Get(entity).Value;
                ref var maxValue = ref ammoMaxPool.Get(entity).Value;

                var newValue = currentValue + deltaValue;

                if (newValue > maxValue)
                {
                    requestPool.Del(entity);
                }
            }
        }
    }
}
