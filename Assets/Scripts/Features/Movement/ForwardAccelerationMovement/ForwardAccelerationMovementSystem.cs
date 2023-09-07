using AsteroidsProject.Features.Core;
using AsteroidsProject.Infrastructure.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.Features.ForwardAccelerationMovement
{
    public class ForwardAccelerationMovementSystem : IEcsRunSystem
    {
        private readonly ITimeService timeService;

        public ForwardAccelerationMovementSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<ForwardAccelerationRequest>()
                              .Inc<ForwardAcceleration>()
                              .Inc<Velocity>()
                              .Inc<Position>()
                              .Inc<Rotation>()
                              .End();

            var accelerationCommandPool = world.GetPool<ForwardAccelerationRequest>();
            var accelerationPool = world.GetPool<ForwardAcceleration>();
            var velocityPool = world.GetPool<Velocity>();
            var rotationPool = world.GetPool<Rotation>();
            var positionPool = world.GetPool<Position>();

            foreach (var entity in filter)
            {
                ref var acceleration = ref accelerationPool.Get(entity).Value;
                ref var velocity = ref velocityPool.Get(entity).Value;
                ref var rotation = ref rotationPool.Get(entity).Value;
                ref var position = ref positionPool.Get(entity).Value;

                var accelerationVector = (Vector2)(rotation * Vector2.up * acceleration);

                velocity += accelerationVector * timeService.DeltaTime;
                position += velocity * timeService.DeltaTime + accelerationVector * Mathf.Pow(timeService.DeltaTime, 2) / 2;

                accelerationCommandPool.Del(entity);
            }
        }
    }
}