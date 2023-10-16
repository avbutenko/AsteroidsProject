using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.DeaccelerationMovement
{
    public class DeaccelerationPositionSystem : IEcsRunSystem
    {
        private readonly ITimeService timeService;

        public DeaccelerationPositionSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CDeaccelerationVector>()
                              .Inc<CVelocity>()
                              .Inc<CPosition>()
                              .End();

            var deaccelerationVectorPool = world.GetPool<CDeaccelerationVector>();
            var velocityPool = world.GetPool<CVelocity>();
            var positionPool = world.GetPool<CPosition>();

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