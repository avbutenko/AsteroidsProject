using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.BasicMovement
{
    public class BasicMovementSystem : IEcsRunSystem
    {
        private readonly ITimeService timeService;

        public BasicMovementSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<Velocity>()
                              .Inc<Position>()
                              .Exc<Acceleration>()
                              .End();

            var velocityPool = world.GetPool<Velocity>();
            var positionPool = world.GetPool<Position>();

            foreach (var entity in filter)
            {
                ref var velocity = ref velocityPool.Get(entity).Value;
                ref var position = ref positionPool.Get(entity).Value;

                position += velocity * timeService.DeltaTime;
            }
        }
    }
}
