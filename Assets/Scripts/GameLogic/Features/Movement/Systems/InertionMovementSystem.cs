using AsteroidsProject.Infrastructure.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class InertionMovementSystem : IEcsRunSystem
    {
        private readonly ITimeService timeService;

        public InertionMovementSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<InertionRequest>()
                              .Inc<InertionModifier>()
                              .Inc<Velocity>()
                              .Inc<Position>()
                              .End();

            var inertionCommandPool = world.GetPool<InertionRequest>();
            var inertionPool = world.GetPool<InertionModifier>();
            var velocityPool = world.GetPool<Velocity>();
            var positionPool = world.GetPool<Position>();

            foreach (var entity in filter)
            {
                ref var velocity = ref velocityPool.Get(entity).Value;

                if (velocity.y <= 0)
                {
                    velocity = Vector2.zero;
                    inertionCommandPool.Del(entity);
                    return;
                }

                ref var inertion = ref inertionPool.Get(entity).Value;
                ref var position = ref positionPool.Get(entity).Value;

                position += velocity * timeService.DeltaTime + Mathf.Pow(timeService.DeltaTime, 2) * inertion / 2;
                velocity += inertion * timeService.DeltaTime;

                inertionCommandPool.Del(entity);
            }
        }
    }
}