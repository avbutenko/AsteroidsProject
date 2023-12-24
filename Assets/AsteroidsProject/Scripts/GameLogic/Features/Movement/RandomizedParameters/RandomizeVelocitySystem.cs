using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class RandomizeVelocitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CRandomizeVelocityRequest> requestPool;
        private EcsPool<CVelocity> velocityPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CRandomizeVelocityRequest>().End();
            requestPool = world.GetPool<CRandomizeVelocityRequest>();
            velocityPool = world.GetPool<CVelocity>();
        }

        public void Run(IEcsSystems systems)
        {
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