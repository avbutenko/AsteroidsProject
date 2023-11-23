using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.MaxVelocityMagnitude
{
    public class MaxVelocityMagnitudeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CMaxVelocityMagnitude> limitPool;
        private EcsPool<CVelocity> velocityPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CMaxVelocityMagnitude>()
                          .Inc<CVelocity>()
                          .End();

            limitPool = world.GetPool<CMaxVelocityMagnitude>();
            velocityPool = world.GetPool<CVelocity>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var limit = ref limitPool.Get(entity).Value;
                ref var velocity = ref velocityPool.Get(entity).Value;

                if (velocity.magnitude > limit)
                {
                    velocity = Vector2.ClampMagnitude(velocity, limit);
                }
            }
        }
    }
}