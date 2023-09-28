using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.AlignVelocityWithRotation
{
    public class AlignVelocityWithRotationSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<AlignVelocityWithRotationRequest>()
                              .Inc<Velocity>()
                              .Inc<Rotation>()
                              .End();

            var requestPool = world.GetPool<AlignVelocityWithRotationRequest>();
            var velocityPool = world.GetPool<Velocity>();
            var rotationPool = world.GetPool<Rotation>();

            foreach (var entity in filter)
            {
                ref var velocity = ref velocityPool.Get(entity).Value;
                ref var rotation = ref rotationPool.Get(entity).Value;

                velocity = (Vector2)(rotation * velocity);
                requestPool.Del(entity);
            }
        }
    }
}