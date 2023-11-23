using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Movement.Basic
{
    public class BasicMovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ITimeService timeService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CVelocity> velocityPool;
        private EcsPool<CPosition> positionPool;

        public BasicMovementSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CVelocity>()
                          .Inc<CPosition>()
                          .Exc<CAccelerationVector>()
                          .Exc<CDeaccelerationVector>()
                          .End();

            velocityPool = world.GetPool<CVelocity>();
            positionPool = world.GetPool<CPosition>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var velocity = ref velocityPool.Get(entity).Value;
                ref var position = ref positionPool.Get(entity).Value;

                position += velocity * timeService.DeltaTime;
            }
        }
    }
}
