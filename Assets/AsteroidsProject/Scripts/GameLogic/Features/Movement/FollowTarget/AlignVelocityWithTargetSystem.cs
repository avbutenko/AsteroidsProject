using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class AlignVelocityWithTargetSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ITimeService timeService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CFollow> followPool;
        private EcsPool<CVelocity> velocityPool;
        private EcsPool<CPosition> positionPool;
        private EcsPool<CRotationSpeed> rotationSpeedPool;

        public AlignVelocityWithTargetSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CFollow>()
                          .Inc<CVelocity>()
                          .Inc<CPosition>()
                          .Inc<CRotationSpeed>()
                          .End();

            followPool = world.GetPool<CFollow>();
            velocityPool = world.GetPool<CVelocity>();
            positionPool = world.GetPool<CPosition>();
            rotationSpeedPool = world.GetPool<CRotationSpeed>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var velocity = ref velocityPool.Get(entity).Value;
                ref var position = ref positionPool.Get(entity).Value;
                ref var rotationSpeed = ref rotationSpeedPool.Get(entity).Value;
                ref var packedTarget = ref followPool.Get(entity).Target;

                if (packedTarget.Unpack(world, out int targetEntity))
                {
                    ref var targetPosition = ref positionPool.Get(targetEntity).Value;
                    var direction = targetPosition - position;
                    var angle = Vector2.SignedAngle(velocity, direction);
                    var currentVelocityRotation = Quaternion.Euler(velocity);
                    var targetVelocityRotation = Quaternion.Euler(0, 0, angle);
                    var rotation = Quaternion.RotateTowards(currentVelocityRotation, targetVelocityRotation, rotationSpeed * timeService.DeltaTime);
                    velocity = rotation * velocity;
                }
            }
        }
    }
}