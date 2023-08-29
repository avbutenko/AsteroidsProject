using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class InertionModifierSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<InertionRequest>()
                              .Inc<Acceleration>()
                              .End();

            var inertCommandPool = world.GetPool<InertionRequest>();
            var accelerationPool = world.GetPool<Acceleration>();

            foreach (var entity in filter)
            {
                ref var acceleration = ref accelerationPool.Get(entity).Value;
                acceleration = Vector2.down;
                inertCommandPool.Del(entity);
            }
        }
    }
}