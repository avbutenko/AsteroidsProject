using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class AccelerationModifierSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<AccelerationRequest>()
                              .Inc<Acceleration>()
                              .End();

            var accelerateCommandPool = world.GetPool<AccelerationRequest>();
            var accelerationPool = world.GetPool<Acceleration>();

            foreach (var entity in filter)
            {
                ref var acceleration = ref accelerationPool.Get(entity).Value;
                acceleration = Vector2.up;
                accelerateCommandPool.Del(entity);
            }
        }
    }
}