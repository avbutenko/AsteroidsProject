using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Rotation
{
    public class RotationSystem : IEcsRunSystem
    {
        private const float MAX_ROTATION_DEGREE = 360f;
        private readonly ITimeService timeService;

        public RotationSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CRotationDirection>()
                              .Inc<CRotationSpeed>()
                              .Inc<CRotation>()
                              .End();

            var rotationDirectionPool = world.GetPool<CRotationDirection>();
            var rotationSpeedPool = world.GetPool<CRotationSpeed>();
            var rotationPool = world.GetPool<CRotation>();

            foreach (var entity in filter)
            {
                ref var rotationDirection = ref rotationDirectionPool.Get(entity).Value;
                ref var rotationSpeed = ref rotationSpeedPool.Get(entity).Value;
                ref var rotation = ref rotationPool.Get(entity).Value;

                rotation = GetNewRotation(rotation, rotationDirection, rotationSpeed);
                rotationDirectionPool.Del(entity);
            }
        }

        public Quaternion GetNewRotation(Quaternion currentRotation, float rotationDirection, float rotationSpeed)
        {
            float deltaValue = rotationDirection * timeService.DeltaTime * rotationSpeed;
            float newValue = Mathf.Repeat(currentRotation.eulerAngles.z + deltaValue, MAX_ROTATION_DEGREE);
            Quaternion quaternion = Quaternion.Euler(0, 0, newValue);
            return quaternion;
        }
    }
}