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
            var filter = world.Filter<AccelerationRequest>()
                              .Inc<Acceleration>()
                              .Inc<Velocity>()
                              .End();

            var accelerationRequestPool = world.GetPool<AccelerationRequest>();
            var accelerationPool = world.GetPool<Acceleration>();
            var velocityPool = world.GetPool<Velocity>();

            foreach (var entity in filter)
            {
                ref var accelerationVector = ref accelerationPool.Get(entity).Vector;
                ref var velocity = ref velocityPool.Get(entity).Value;

                velocity += accelerationVector * timeService.DeltaTime;
            }
        }
    }
}