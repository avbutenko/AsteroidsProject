using AsteroidsProject.Infrastructure.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class DeaccelerationMovementSystem : IEcsRunSystem
    {
        private readonly ITimeService timeService;

        public DeaccelerationMovementSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<DeaccelerationRequest>()
                              .Inc<Deacceleration>()
                              .Inc<Velocity>()
                              .Inc<Position>()
                              .End();

            var deaccelerationCommandPool = world.GetPool<DeaccelerationRequest>();
            var deaccelerationPool = world.GetPool<Deacceleration>();
            var velocityPool = world.GetPool<Velocity>();
            var positionPool = world.GetPool<Position>();

            foreach (var entity in filter)
            {
                ref var velocity = ref velocityPool.Get(entity).Value;

                if (velocity == Vector2.zero) return;

                ref var deacceleration = ref deaccelerationPool.Get(entity).Value;
                ref var position = ref positionPool.Get(entity).Value;

                var deaccelerationVector = velocity.normalized * deacceleration;
                var newVelocity = velocity + deaccelerationVector * timeService.DeltaTime;

                if (IsOppositeVectors(newVelocity, velocity))
                {
                    velocity = newVelocity;
                    position += velocity * timeService.DeltaTime + deaccelerationVector * Mathf.Pow(timeService.DeltaTime, 2) / 2;
                }
                else
                {
                    velocity = Vector2.zero;
                }

                deaccelerationCommandPool.Del(entity);
            }
        }

        private bool IsOppositeVectors(Vector2 a, Vector2 b)
        {
            return Vector2.Dot(a.normalized, b.normalized) > 0;
        }
    }
}