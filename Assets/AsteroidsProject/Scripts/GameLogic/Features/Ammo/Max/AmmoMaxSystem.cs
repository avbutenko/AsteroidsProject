using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Ammo.Max
{
    public class AmmoMaxSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var filter = world.Filter<CAmmoMax>()
                              .Inc<CAmmo>()
                              .Inc<CChangeAmmoAmountRequest>()
                              .End();

            var ammoMaxPool = world.GetPool<CAmmoMax>();
            var ammoPool = world.GetPool<CAmmo>();
            var requestPool = world.GetPool<CChangeAmmoAmountRequest>();

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
