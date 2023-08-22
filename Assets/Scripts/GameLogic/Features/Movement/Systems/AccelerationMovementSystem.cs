using AsteroidsProject.Infrastructure.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class AccelerationMovementSystem : IEcsRunSystem
    {
        private readonly ITimeService timeService;

        public AccelerationMovementSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<AccelerationCommand>()
                //.Inc<MovementSpeed>()
                .Inc<MovementMaxSpeed>()
                .Inc<MovementCurrentSpeed>()
                .Inc<MovementDirection>()
                .Inc<MovementAccelerationModifier>()
                .Inc<Position>()
                .Inc<Rotation.Rotation>()
                .End();

            var accelerationCommandPool = world.GetPool<AccelerationCommand>();
            //var movementSpeedPool = world.GetPool<MovementSpeed>();
            var movementMaxSpeedPool = world.GetPool<MovementMaxSpeed>();
            var movementCurrentSpeedPool = world.GetPool<MovementCurrentSpeed>();
            var movementDirectionPool = world.GetPool<MovementDirection>();
            var accelerationModifierPool = world.GetPool<MovementAccelerationModifier>();
            var positionPool = world.GetPool<Position>();
            var rotationPool = world.GetPool<Rotation.Rotation>();


            foreach (var entity in filter)
            {
                //ref var speed = ref movementSpeedPool.Get(entity).Value;
                ref var maxSpeed = ref movementMaxSpeedPool.Get(entity).Value;
                ref var currentSpeed = ref movementCurrentSpeedPool.Get(entity).Value;
                ref var direction = ref movementDirectionPool.Get(entity).Value;
                ref var accelerationModifier = ref accelerationModifierPool.Get(entity).Value;
                ref var currentPosition = ref positionPool.Get(entity).Value;
                ref var rotation = ref rotationPool.Get(entity).Value;

                currentSpeed = GetNewSpeed(ref currentSpeed, ref accelerationModifier, ref maxSpeed);
                currentPosition = GetNewPosition(ref currentSpeed, ref direction, ref rotation, ref currentPosition);

                accelerationCommandPool.Del(entity);
            }

        }

        private float GetNewSpeed(ref float currentSpeed, ref float accelerationModifier, ref float maxSpeed)
        {
            var newSpeed = currentSpeed + accelerationModifier * timeService.DeltaTime;
            newSpeed = Mathf.Clamp(newSpeed, 0f, maxSpeed);
            return newSpeed;
        }

        private Vector2 GetNewPosition(ref float currentSpeed, ref Vector2 direction, ref Quaternion rotation, ref Vector2 currentPosition)
        {
            Vector2 moveDirection = rotation * direction;
            var newPosition = currentPosition + currentSpeed * timeService.DeltaTime * moveDirection.normalized;
            return newPosition;
        }
    }
}