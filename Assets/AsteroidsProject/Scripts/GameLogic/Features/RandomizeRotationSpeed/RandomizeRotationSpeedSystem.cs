using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.RandomizeRotationSpeed
{
    public class RandomizeRotationSpeedSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<RandomizeRotationSpeedRequest>()
                              .Inc<RotationSpeed>()
                              .End();

            var randomizeRotationSpeedRequestPool = world.GetPool<RandomizeRotationSpeedRequest>();
            var rotationSpeedPool = world.GetPool<RotationSpeed>();

            foreach (var entity in filter)
            {
                ref var min = ref randomizeRotationSpeedRequestPool.Get(entity).Min;
                ref var max = ref randomizeRotationSpeedRequestPool.Get(entity).Max;
                ref var rotationSpeed = ref rotationSpeedPool.Get(entity).Value;

                rotationSpeed = Random.Range(min, max);
                randomizeRotationSpeedRequestPool.Del(entity);
            }
        }
    }
}