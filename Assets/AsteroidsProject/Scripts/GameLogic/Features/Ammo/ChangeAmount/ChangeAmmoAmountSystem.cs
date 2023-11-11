using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Ammo.ChangeAmount
{
    public class ChangeAmmoAmountSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CChangeAmmoAmountRequest>().End();
            var ammoPool = world.GetPool<CAmmo>();
            var requestPool = world.GetPool<CChangeAmmoAmountRequest>();

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

                requestPool.Del(entity);
            }
        }
    }
}
