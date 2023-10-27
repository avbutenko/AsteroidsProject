using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Rotation.Permanent
{
    public class PermanentRotationSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CPermanentRotationDirection>().End();

            var permanentRotationPool = world.GetPool<CPermanentRotationDirection>();
            var rotationDirectionPool = world.GetPool<CRotationDirection>();

            foreach (var entity in filter)
            {
                ref var direction = ref permanentRotationPool.Get(entity).Value;
                rotationDirectionPool.Add(entity).Value = direction;
            }
        }
    }
}