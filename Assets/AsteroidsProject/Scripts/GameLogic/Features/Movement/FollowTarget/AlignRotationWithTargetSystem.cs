using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement.FollowTarget
{
    public class AlignRotationWithTargetSystem : IEcsRunSystem
    {
        private readonly ITimeService timeService;

        public AlignRotationWithTargetSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CFollow>()
                              .Inc<CPosition>()
                              .Inc<CRotation>()
                              .Inc<CRotationSpeed>()
                              .End();

            var followPool = world.GetPool<CFollow>();
            var positionPool = world.GetPool<CPosition>();
            var rotationPool = world.GetPool<CRotation>();
            var rotationSpeedPool = world.GetPool<CRotationSpeed>();

            foreach (var entity in filter)
            {
                ref var position = ref positionPool.Get(entity).Value;
                ref var rotation = ref rotationPool.Get(entity).Value;
                ref var rotationSpeed = ref rotationSpeedPool.Get(entity).Value;
                ref var packedTarget = ref followPool.Get(entity).Target;

                if (packedTarget.Unpack(world, out int targetEntity))
                {
                    ref var targetPosition = ref positionPool.Get(targetEntity).Value;
                    var direction = targetPosition - position;
                    var angle = Vector2.SignedAngle(Vector2.up, direction);
                    var targetRotation = Quaternion.Euler(0, 0, angle);
                    rotation = Quaternion.RotateTowards(rotation, targetRotation, rotationSpeed * timeService.DeltaTime);
                }
            }
        }
    }
}