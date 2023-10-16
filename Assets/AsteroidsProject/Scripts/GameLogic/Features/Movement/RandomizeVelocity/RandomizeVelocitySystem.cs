using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.RandomizeVelocity
{
    public class RandomizeVelocitySystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CRandomizeVelocityRequest>().End();

            var requestPool = world.GetPool<CRandomizeVelocityRequest>();
            var velocityPool = world.GetPool<CVelocity>();

            foreach (var entity in filter)
            {
                ref var range = ref requestPool.Get(entity).Range;
                var xMin = range[0].x;
                var xMax = range[1].x;
                var yMin = range[0].y;
                var yMax = range[1].y;

                velocityPool.Add(entity).Value = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
                requestPool.Del(entity);
            }
        }
    }
}