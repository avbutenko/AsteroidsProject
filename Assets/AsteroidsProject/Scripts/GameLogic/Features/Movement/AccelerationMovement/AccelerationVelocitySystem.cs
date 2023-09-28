using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.AccelerationMovement
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
            var filter = world.Filter<AccelerationVector>()
                              .Inc<Velocity>()
                              .End();

            var accelerationVectorPool = world.GetPool<AccelerationVector>();
            var velocityPool = world.GetPool<Velocity>();

            foreach (var entity in filter)
            {
                ref var accelerationVector = ref accelerationVectorPool.Get(entity).Value;
                ref var velocity = ref velocityPool.Get(entity).Value;

                velocity += accelerationVector * timeService.DeltaTime;
            }
        }
    }
}