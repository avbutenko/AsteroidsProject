using Leopotam.EcsLite;

namespace AsteroidsProject.Features.Core
{
    public class UpdateViewRotationSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<Transform>()
                              .Inc<Rotation>()
                              .End();

            var transformPool = world.GetPool<Transform>();
            var rotationPool = world.GetPool<Rotation>();


            foreach (var entity in filter)
            {
                ref var rotation = ref rotationPool.Get(entity).Value;
                ref var transform = ref transformPool.Get(entity).Value;

                transform.localRotation = rotation;
            }
        }
    }
}