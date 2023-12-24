using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class DeaccelerationVelocitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ITimeService timeService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CDeaccelerationVector> deaccelerationVectorPool;
        private EcsPool<CVelocity> velocityPool;

        public DeaccelerationVelocitySystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CDeaccelerationVector>()
                          .Inc<CVelocity>()
                          .End();

            deaccelerationVectorPool = world.GetPool<CDeaccelerationVector>();
            velocityPool = world.GetPool<CVelocity>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var velocity = ref velocityPool.Get(entity).Value;
                ref var deaccelerationVector = ref deaccelerationVectorPool.Get(entity).Value;

                var newVelocity = velocity + deaccelerationVector * timeService.DeltaTime;

                if (IsOppositeVectors(newVelocity, velocity))
                {
                    velocity = newVelocity;
                }
                else
                {
                    velocity = Vector2.zero;
                    deaccelerationVectorPool.Del(entity);
                }
            }
        }

        private bool IsOppositeVectors(Vector2 a, Vector2 b)
        {
            return Vector2.Dot(a.normalized, b.normalized) > 0;
        }
    }
}