using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement.Acceleration
{
    public class AccelerationVectorSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CAccelerationVector>()
                              .Inc<CAccelerationModifier>()
                              .Inc<CRotation>()
                              .End();

            var accelerationVectorPool = world.GetPool<CAccelerationVector>();
            var accelerationModifierPool = world.GetPool<CAccelerationModifier>();
            var rotationPool = world.GetPool<CRotation>();

            foreach (var entity in filter)
            {
                ref var accelerationVector = ref accelerationVectorPool.Get(entity).Value;
                ref var accelerationModifier = ref accelerationModifierPool.Get(entity).Value;
                ref var rotation = ref rotationPool.Get(entity).Value;

                accelerationVector = (Vector2)(rotation * Vector2.up * accelerationModifier);
            }
        }
    }
}