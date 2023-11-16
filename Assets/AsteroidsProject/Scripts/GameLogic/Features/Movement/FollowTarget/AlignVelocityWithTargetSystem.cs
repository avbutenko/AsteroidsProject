using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement.FollowTarget
{
    public class AlignVelocityWithTargetSystem : IEcsRunSystem
    {
        private readonly ITimeService timeService;

        public AlignVelocityWithTargetSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CFollow>()
                              .Inc<CVelocity>()
                              .Inc<CPosition>()
                              .Inc<CRotationSpeed>()
                              .End();

            var followPool = world.GetPool<CFollow>();
            var velocityPool = world.GetPool<CVelocity>();
            var positionPool = world.GetPool<CPosition>();
            var rotationSpeedPool = world.GetPool<CRotationSpeed>();

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