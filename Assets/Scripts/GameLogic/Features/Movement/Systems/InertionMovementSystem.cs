using AsteroidsProject.Infrastructure.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class InertionMovementSystem : IEcsRunSystem
    {
        private readonly ITimeService timeService;

        public InertionMovementSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<InertionRequest>()
                              .Inc<InertionModifier>()
                              .Inc<Velocity>()
                              .Inc<Position>()
                              .End();

            var inertionCommandPool = world.GetPool<InertionRequest>();
            var inertionPool = world.GetPool<InertionModifier>();
            var velocityPool = world.GetPool<Velocity>();
            var positionPool = world.GetPool<Position>();
            var rotationPool = world.GetPool<Rotation.Rotation>();

            foreach (var entity in filter)
            {
                inertionCommandPool.Del(entity);

                ref var velocity = ref velocityPool.Get(entity).Value;
                ref var inertion = ref inertionPool.Get(entity).Value;
                ref var position = ref positionPool.Get(entity).Value;
                ref var rotation = ref rotationPool.Get(entity).Value;

                var newVelocity = velocity + rotation * inertion * timeService.DeltaTime;

                if (newVelocity.magnitude > velocity.magnitude)
                {
                    velocity = Vector3.zero;
                    return;
                }
                else
                {
                    velocity = newVelocity;
                    position += velocity * timeService.DeltaTime + Mathf.Pow(timeService.DeltaTime, 2) * inertion / 2;
                }
            }
        }
    }
}