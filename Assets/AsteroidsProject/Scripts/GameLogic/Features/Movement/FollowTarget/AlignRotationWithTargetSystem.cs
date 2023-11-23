using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement.FollowTarget
{
    public class AlignRotationWithTargetSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ITimeService timeService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CFollow> followPool;
        private EcsPool<CPosition> positionPool;
        private EcsPool<CRotation> rotationPool;
        private EcsPool<CRotationSpeed> rotationSpeedPool;

        public AlignRotationWithTargetSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CFollow>()
                          .Inc<CPosition>()
                          .Inc<CRotation>()
                          .Inc<CRotationSpeed>()
                          .End();

            followPool = world.GetPool<CFollow>();
            positionPool = world.GetPool<CPosition>();
            rotationPool = world.GetPool<CRotation>();
            rotationSpeedPool = world.GetPool<CRotationSpeed>();
        }

        public void Run(IEcsSystems systems)
        {
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