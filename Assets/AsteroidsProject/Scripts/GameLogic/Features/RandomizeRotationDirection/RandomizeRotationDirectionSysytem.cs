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
            var filter = world.Filter<RandomizeRotationDirectionRequest>().End();

            var randomizeRotationDirectionRequestPool = world.GetPool<RandomizeRotationDirectionRequest>();
            var rotationDirectionPool = world.GetPool<RotationDirection>();

            foreach (var entity in filter)
            {
                rotationDirectionPool.Add(entity).Value = Random.Range(RIGHT, LEFT);
                randomizeRotationDirectionRequestPool.Del(entity);
            }
        }
    }
}