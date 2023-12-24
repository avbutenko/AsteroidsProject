using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class AccelerationVectorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CAccelerationVector> accelerationVectorPool;
        private EcsPool<CAccelerationModifier> accelerationModifierPool;
        private EcsPool<CRotation> rotationPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CAccelerationVector>()
                          .Inc<CAccelerationModifier>()
                          .Inc<CRotation>()
                          .End();

            accelerationVectorPool = world.GetPool<CAccelerationVector>();
            accelerationModifierPool = world.GetPool<CAccelerationModifier>();
            rotationPool = world.GetPool<CRotation>();
        }

        public void Run(IEcsSystems systems)
        {
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