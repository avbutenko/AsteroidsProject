using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement.Acceleration
{
    public class AccelerationPositionSystem : IEcsRunSystem
    {
        private readonly ITimeService timeService;

        public AccelerationPositionSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CAccelerationVector>()
                              .Inc<CVelocity>()
                              .Inc<CPosition>()
                              .End();

            var accelerationVectorPool = world.GetPool<CAccelerationVector>();
            var velocityPool = world.GetPool<CVelocity>();
            var positionPool = world.GetPool<CPosition>();

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