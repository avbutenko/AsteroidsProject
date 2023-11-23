using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement.Acceleration
{
    public class AccelerationPositionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ITimeService timeService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CAccelerationVector> accelerationVectorPool;
        private EcsPool<CVelocity> velocityPool;
        private EcsPool<CPosition> positionPool;

        public AccelerationPositionSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CAccelerationVector>()
                          .Inc<CVelocity>()
                          .Inc<CPosition>()
                          .End();

            accelerationVectorPool = world.GetPool<CAccelerationVector>();
            velocityPool = world.GetPool<CVelocity>();
            positionPool = world.GetPool<CPosition>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var accelerationVector = ref accelerationVectorPool.Get(entity).Value;
                ref var velocity = ref velocityPool.Get(entity).Value;
                ref var position = ref positionPool.Get(entity).Value;

                position += velocity * timeService.DeltaTime + accelerationVector * Mathf.Pow(timeService.DeltaTime, 2) / 2;
            }
        }

    }
}