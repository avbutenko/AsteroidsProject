using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Movement.Acceleration
{
    public class AccelerationVelocitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ITimeService timeService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CAccelerationVector> accelerationVectorPool;
        private EcsPool<CVelocity> velocityPool;

        public AccelerationVelocitySystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CAccelerationVector>()
                          .Inc<CVelocity>()
                          .End();

            accelerationVectorPool = world.GetPool<CAccelerationVector>();
            velocityPool = world.GetPool<CVelocity>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var accelerationVector = ref accelerationVectorPool.Get(entity).Value;
                ref var velocity = ref velocityPool.Get(entity).Value;

                velocity += accelerationVector * timeService.DeltaTime;
            }
        }
    }
}