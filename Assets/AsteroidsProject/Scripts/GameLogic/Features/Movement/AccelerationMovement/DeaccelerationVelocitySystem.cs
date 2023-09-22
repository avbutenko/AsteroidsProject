﻿using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.AccelerationMovement
{
    public class DeaccelerationVelocitySystem : IEcsRunSystem
    {
        private readonly ITimeService timeService;

        public DeaccelerationVelocitySystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<DeaccelerationRequest>()
                              .Inc<Acceleration>()
                              .Inc<Velocity>()
                              .End();

            var deaccelerationRequestPool = world.GetPool<DeaccelerationRequest>();
            var deaccelerationPool = world.GetPool<Acceleration>();
            var velocityPool = world.GetPool<Velocity>();

            foreach (var entity in filter)
            {
                ref var velocity = ref velocityPool.Get(entity).Value;
                ref var deaccelerationVector = ref deaccelerationPool.Get(entity).Vector;

                var newVelocity = velocity + deaccelerationVector * timeService.DeltaTime;

                if (IsOppositeVectors(newVelocity, velocity))
                {
                    velocity = newVelocity;
                }
                else
                {
                    velocity = Vector2.zero;
                    deaccelerationVector = Vector2.zero;
                    deaccelerationRequestPool.Del(entity);
                }
            }
        }

        private bool IsOppositeVectors(Vector2 a, Vector2 b)
        {
            return Vector2.Dot(a.normalized, b.normalized) > 0;
        }
    }
}