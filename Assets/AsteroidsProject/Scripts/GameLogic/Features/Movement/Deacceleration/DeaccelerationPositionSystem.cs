using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement.Deacceleration
{
    public class DeaccelerationPositionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ITimeService timeService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CDeaccelerationVector> deaccelerationVectorPool;
        private EcsPool<CVelocity> velocityPool;
        private EcsPool<CPosition> positionPool;

        public DeaccelerationPositionSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CDeaccelerationVector>()
                          .Inc<CVelocity>()
                          .Inc<CPosition>()
                          .End();

            deaccelerationVectorPool = world.GetPool<CDeaccelerationVector>();
            velocityPool = world.GetPool<CVelocity>();
            positionPool = world.GetPool<CPosition>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var deaccelerationVector = ref deaccelerationVectorPool.Get(entity).Value;
                ref var velocity = ref velocityPool.Get(entity).Value;
                ref var position = ref positionPool.Get(entity).Value;

                position += velocity * timeService.DeltaTime + deaccelerationVector * Mathf.Pow(timeService.DeltaTime, 2) / 2;
            }
        }

    }
}