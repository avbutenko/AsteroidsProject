﻿using AsteroidsProject.Infrastructure.Services;
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
            var filter = world.Filter<AccelerationRequest>()
                              .Inc<AccelerationModifier>()
                              .Inc<Velocity>()
                              .Inc<Position>()
                              .Inc<MovingDirectionQuaternion>()
                              .End();

            var accelerateCommandPool = world.GetPool<AccelerationRequest>();
            var accelerationPool = world.GetPool<AccelerationModifier>();
            var velocityPool = world.GetPool<Velocity>();
            var positionPool = world.GetPool<Position>();
            var rotationPool = world.GetPool<Rotation.Rotation>();
            var directionQuaternionPool = world.GetPool<MovingDirectionQuaternion>();

            foreach (var entity in filter)
            {
                ref var acceleration = ref accelerationPool.Get(entity).Value;
                ref var position = ref positionPool.Get(entity).Value;
                ref var velocity = ref velocityPool.Get(entity).Value;
                ref var rotation = ref rotationPool.Get(entity).Value;
                ref var directionQuaternion = ref directionQuaternionPool.Get(entity).Value;

                velocity += rotation * acceleration * timeService.DeltaTime;
                position += velocity * timeService.DeltaTime + (Mathf.Pow(timeService.DeltaTime, 2) * (rotation * acceleration)) / 2;
                directionQuaternion = rotation;

                accelerateCommandPool.Del(entity);
            }
        }
    }
}