using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.Features.Core
{
    public class UpdateViewScaleSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<Transform>()
                              .Inc<Scale>()
                              .End();

            var transformPool = world.GetPool<Transform>();
            var scalePool = world.GetPool<Scale>();

            foreach (var entity in filter)
            {
                ref var scale = ref scalePool.Get(entity).Value;
                ref var transform = ref transformPool.Get(entity).Value;

                transform.localScale = new Vector3(scale, scale, scale);
            }
        }
    }
}