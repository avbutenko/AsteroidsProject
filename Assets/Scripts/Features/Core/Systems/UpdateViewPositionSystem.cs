using Leopotam.EcsLite;

namespace AsteroidsProject.Features.Core
{
    public class UpdateViewPositionSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<Transform>()
                              .Inc<Position>()
                              .End();

            var transformPool = world.GetPool<Transform>();
            var positionPool = world.GetPool<Position>();


            foreach (var entity in filter)
            {
                ref var position = ref positionPool.Get(entity).Value;
                ref var transform = ref transformPool.Get(entity).Value;

                transform.localPosition = position;
            }
        }
    }
}