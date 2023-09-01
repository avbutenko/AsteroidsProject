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
                              .Inc<MovingDirectionQuaternion>()
                              .End();

            var inertionCommandPool = world.GetPool<InertionRequest>();
            var inertionPool = world.GetPool<InertionModifier>();
            var velocityPool = world.GetPool<Velocity>();
            var positionPool = world.GetPool<Position>();
            var directionQuaternionPool = world.GetPool<MovingDirectionQuaternion>();

            foreach (var entity in filter)
            {
                inertionCommandPool.Del(entity);

                ref var velocity = ref velocityPool.Get(entity).Value;
                ref var inertion = ref inertionPool.Get(entity).Value;
                ref var position = ref positionPool.Get(entity).Value;
                ref var directionQuaternion = ref directionQuaternionPool.Get(entity).Value;

                var newVelocity = velocity + directionQuaternion * inertion * timeService.DeltaTime;


                var dotResult = Vector3.Dot(newVelocity, velocity);
                Debug.Log("newVelocity.normalized: " + newVelocity.normalized);
                Debug.Log("velocity.normalized: " + velocity.normalized);
                Debug.Log("dotResult: " + dotResult);

                if (dotResult > 0)
                {
                    velocity = newVelocity;
                    position += velocity * timeService.DeltaTime + (Mathf.Pow(timeService.DeltaTime, 2) * (directionQuaternion * inertion)) / 2;
                }
                else
                {
                    velocity = Vector3.zero;
                }
            }
        }
    }
}