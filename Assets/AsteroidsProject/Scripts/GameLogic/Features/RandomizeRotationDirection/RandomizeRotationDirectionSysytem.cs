using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.RandomizeRotationDirection
{
    public class RandomizeRotationDirectionSysytem : IEcsRunSystem
    {
        private const int LEFT = 1;
        private const int RIGHT = -1;

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<RandomizeRotationDirectionRequest>()
                              .Inc<RotationDirection>()
                              .End();

            var randomizeRotationDirectionRequestPool = world.GetPool<RandomizeRotationDirectionRequest>();
            var rotationDirectionPool = world.GetPool<RotationDirection>();

            foreach (var entity in filter)
            {
                ref var rotationDirection = ref rotationDirectionPool.Get(entity).Value;
                rotationDirection = Random.Range(RIGHT, LEFT);
                randomizeRotationDirectionRequestPool.Del(entity);
            }
        }
    }
}