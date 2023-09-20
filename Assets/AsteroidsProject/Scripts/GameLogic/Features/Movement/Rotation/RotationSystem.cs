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
            var filter = world.Filter<RotationDirection>()
                              .Inc<RotationSpeed>()
                              .Inc<Core.Rotation>()
                              .End();

            var rotationDirectionPool = world.GetPool<RotationDirection>();
            var rotationSpeedPool = world.GetPool<RotationSpeed>();
            var rotationPool = world.GetPool<Core.Rotation>();

            foreach (var entity in filter)
            {
                ref var rotationDirection = ref rotationDirectionPool.Get(entity).Value;
                ref var rotationSpeed = ref rotationSpeedPool.Get(entity).Value;
                ref var rotation = ref rotationPool.Get(entity).Value;

                rotation = GetNewRotation(rotation, rotationDirection, rotationSpeed);
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