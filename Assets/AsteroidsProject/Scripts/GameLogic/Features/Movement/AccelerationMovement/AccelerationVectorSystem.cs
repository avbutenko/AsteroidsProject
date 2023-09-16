﻿using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.AccelerationMovement
{
    public class AccelerationVectorSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<AccelerationRequest>()
                              .Inc<Acceleration>()
                              .Inc<Rotation>()
                              .End();

            var accelerationPool = world.GetPool<Acceleration>();
            var rotationPool = world.GetPool<Rotation>();

            foreach (var entity in filter)
            {
                ref var accelerationVector = ref accelerationPool.Get(entity).Vector;
                ref var accelerationModifier = ref accelerationPool.Get(entity).AccelerationModifier;
                ref var rotation = ref rotationPool.Get(entity).Value;

                accelerationVector = (Vector2)(rotation * Vector2.up * accelerationModifier);
            }
        }
    }
}