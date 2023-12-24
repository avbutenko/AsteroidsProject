using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Damage
{
    public class DamageSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CHealth> healthPool;
        private EcsPool<CDamageRequest> damageRequestPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            filter = world.Filter<CDamageRequest>()
                          .Inc<CHealth>()
                          .End();

            healthPool = world.GetPool<CHealth>();
            damageRequestPool = world.GetPool<CDamageRequest>();
        }

        public void Run(IEcsSystems systems)
        {
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