using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.AccelerationMovement
{
    public class DeaccelerationVectorSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<DeaccelerationRequest>()
                              .Inc<Acceleration>()
                              .Inc<Velocity>()
                              .End();

            var accelerationPool = world.GetPool<Acceleration>();
            var velocityPool = world.GetPool<Velocity>();

            foreach (var entity in filter)
            {
                ref var deaccelerationVector = ref accelerationPool.Get(entity).Vector;
                ref var deaccelerationModifier = ref accelerationPool.Get(entity).DeaccelerationModifier;
                ref var velocity = ref velocityPool.Get(entity).Value;

                deaccelerationVector = velocity.normalized * deaccelerationModifier;
            }
        }
    }
}