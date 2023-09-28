using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.DeaccelerationMovement
{
    public class DeaccelerationVectorSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<DeaccelerationVector>()
                              .Inc<DeaccelerationModifier>()
                              .Inc<Velocity>()
                              .End();

            var deaccelerationVectorPool = world.GetPool<DeaccelerationVector>();
            var deaccelerationModifierPool = world.GetPool<DeaccelerationModifier>();
            var velocityPool = world.GetPool<Velocity>();

            foreach (var entity in filter)
            {
                ref var deaccelerationVector = ref deaccelerationVectorPool.Get(entity).Value;
                ref var deaccelerationModifier = ref deaccelerationModifierPool.Get(entity).Value;
                ref var velocity = ref velocityPool.Get(entity).Value;

                deaccelerationVector = velocity.normalized * deaccelerationModifier;
            }
        }
    }
}