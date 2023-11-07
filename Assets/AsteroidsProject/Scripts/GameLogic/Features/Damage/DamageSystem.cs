using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Damage
{
    public class DamageSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var filter = world.Filter<CDamageRequest>()
                              .Inc<CHealth>()
                              .End();

            var healthPool = world.GetPool<CHealth>();
            var damageRequestPool = world.GetPool<CDamageRequest>();


            foreach (var entity in filter)
            {
                ref var currentValue = ref healthPool.Get(entity).Value;
                ref var deltaValue = ref damageRequestPool.Get(entity).Value;

                currentValue += deltaValue;

                if (currentValue <= 0)
                {
                    world.NewEntityWith(new CDeathEvent { PackedEntity = world.PackEntity(entity) });
                }

                damageRequestPool.Del(entity);
            }
        }
    }
}