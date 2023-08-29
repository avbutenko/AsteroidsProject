using AsteroidsProject.Infrastructure.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class UniformAccelerationMovementSystem : IEcsRunSystem
    {
        private readonly ITimeService timeService;

        public UniformAccelerationMovementSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<Velocity>()
                              .Inc<Acceleration>()
                              .Inc<GameplayObjectViewComponent>()
                              .End();

            var accelerationPool = world.GetPool<Acceleration>();
            var velocityPool = world.GetPool<Velocity>();
            var objectsPool = world.GetPool<GameplayObjectViewComponent>();

            foreach (var entity in filter)
            {
                ref var acceleration = ref accelerationPool.Get(entity).Value;

                if (acceleration == Vector2.zero) return;

                ref var velocity = ref velocityPool.Get(entity).Value;
                ref var position = ref objectsPool.Get(entity).Position;

                position = position +
                   velocity * timeService.DeltaTime +
                   Mathf.Pow(timeService.DeltaTime, 2) * acceleration / 2;

                velocity += acceleration * timeService.DeltaTime;

                if (velocity.magnitude <= 0)
                {
                    acceleration = Vector2.zero;
                }
            }
        }
    }
}