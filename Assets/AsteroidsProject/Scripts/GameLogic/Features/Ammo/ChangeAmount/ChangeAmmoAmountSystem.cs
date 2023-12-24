using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Ammo
{
    public class ChangeAmmoAmountSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CAmmo> ammoPool;
        private EcsPool<CChangeAmmoAmountRequest> requestPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CChangeAmmoAmountRequest>().End();
            ammoPool = world.GetPool<CAmmo>();
            requestPool = world.GetPool<CChangeAmmoAmountRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var deltaValue = ref requestPool.Get(entity).Value;

                if (ammoPool.Has(entity))
                {
                    ref var currentValue = ref ammoPool.Get(entity).Value;
                    currentValue += deltaValue;
                }
                else
                {
                    ammoPool.Add(entity).Value = deltaValue;
                }
            }
        }
    }
}
