using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.AccelerationMovement
{
    public class AccelerationVectorSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<AccelerationVector>()
                              .Inc<AccelerationModifier>()
                              .Inc<Rotation>()
                              .End();

            var accelerationVectorPool = world.GetPool<AccelerationVector>();
            var accelerationModifierPool = world.GetPool<AccelerationModifier>();
            var rotationPool = world.GetPool<Rotation>();

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