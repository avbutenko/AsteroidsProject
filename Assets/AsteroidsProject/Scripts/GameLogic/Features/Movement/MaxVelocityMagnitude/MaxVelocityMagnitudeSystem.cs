using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.MaxVelocityMagnitude
{
    public class MaxVelocityMagnitudeSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CMaxVelocityMagnitude>()
                              .Inc<CVelocity>()
                              .End();

            var limitPool = world.GetPool<CMaxVelocityMagnitude>();
            var velocityPool = world.GetPool<CVelocity>();

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