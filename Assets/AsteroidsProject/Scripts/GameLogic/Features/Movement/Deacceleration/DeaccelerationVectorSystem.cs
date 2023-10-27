using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Movement.Deacceleration
{
    public class DeaccelerationVectorSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CDeaccelerationVector>()
                              .Inc<CDeaccelerationModifier>()
                              .Inc<CVelocity>()
                              .End();

            var deaccelerationVectorPool = world.GetPool<CDeaccelerationVector>();
            var deaccelerationModifierPool = world.GetPool<CDeaccelerationModifier>();
            var velocityPool = world.GetPool<CVelocity>();

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