using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class BasicRotationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const float MAX_ROTATION_DEGREE = 360f;
        private readonly ITimeService timeService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CRotationDirection> rotationDirectionPool;
        private EcsPool<CRotationSpeed> rotationSpeedPool;
        private EcsPool<CRotation> rotationPool;

        public BasicRotationSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CRotationDirection>()
                              .Inc<CRotationSpeed>()
                              .Inc<CRotation>()
                              .End();

            rotationDirectionPool = world.GetPool<CRotationDirection>();
            rotationSpeedPool = world.GetPool<CRotationSpeed>();
            rotationPool = world.GetPool<CRotation>();
        }

        public void Run(IEcsSystems systems)
        {
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