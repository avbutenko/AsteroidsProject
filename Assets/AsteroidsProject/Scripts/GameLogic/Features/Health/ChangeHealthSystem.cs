using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Health
{
    public class ChangeHealthSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var filter = world.Filter<CChangeHealthRequest>()
                              .Inc<CHealth>()
                              .End();

            var healthPool = world.GetPool<CHealth>();
            var requestPool = world.GetPool<CChangeHealthRequest>();

            foreach (var entity in filter)
            {
                ref var currentValue = ref healthPool.Get(entity).Value;
                ref var deltaValue = ref requestPool.Get(entity).Value;

                currentValue += deltaValue;
                requestPool.Del(entity);
            }
        }
    }
}