using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Movement.Acceleration
{
    public class AccelerationVelocitySystem : IEcsRunSystem
    {
        private readonly ITimeService timeService;

        public AccelerationVelocitySystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CAccelerationVector>()
                              .Inc<CVelocity>()
                              .End();

            var accelerationVectorPool = world.GetPool<CAccelerationVector>();
            var velocityPool = world.GetPool<CVelocity>();

            foreach (var entity in filter)
            {
                ref var accelerationVector = ref accelerationVectorPool.Get(entity).Value;
                ref var velocity = ref velocityPool.Get(entity).Value;

                velocity += accelerationVector * timeService.DeltaTime;
            }
        }
    }
}