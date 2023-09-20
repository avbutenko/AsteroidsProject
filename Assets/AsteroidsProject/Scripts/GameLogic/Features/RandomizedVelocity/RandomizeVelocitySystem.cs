using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.RandomizedVelocity
{
    public class RandomizeVelocitySystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<RandomizeVelocityRequest>()
                              .Inc<Velocity>()
                              .End();

            var randomizeVelocityRequestPool = world.GetPool<RandomizeVelocityRequest>();
            var velocityPool = world.GetPool<Velocity>();

            foreach (var entity in filter)
            {
                ref var velocity = ref velocityPool.Get(entity).Value;
                ref var xMin = ref randomizeVelocityRequestPool.Get(entity).VelocityRange.XMin;
                ref var xMax = ref randomizeVelocityRequestPool.Get(entity).VelocityRange.XMax;
                ref var yMin = ref randomizeVelocityRequestPool.Get(entity).VelocityRange.YMin;
                ref var yMax = ref randomizeVelocityRequestPool.Get(entity).VelocityRange.YMax;

                velocity.x = Random.Range(xMin, xMax);
                velocity.y = Random.Range(yMin, yMax);

                randomizeVelocityRequestPool.Del(entity);
            }
        }
    }
}