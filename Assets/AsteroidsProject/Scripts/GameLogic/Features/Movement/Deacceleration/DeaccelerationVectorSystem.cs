using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class DeaccelerationVectorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CDeaccelerationVector> deaccelerationVectorPool;
        private EcsPool<CDeaccelerationModifier> deaccelerationModifierPool;
        private EcsPool<CVelocity> velocityPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CDeaccelerationVector>()
                              .Inc<CDeaccelerationModifier>()
                              .Inc<CVelocity>()
                              .End();

            deaccelerationVectorPool = world.GetPool<CDeaccelerationVector>();
            deaccelerationModifierPool = world.GetPool<CDeaccelerationModifier>();
            velocityPool = world.GetPool<CVelocity>();
        }

        public void Run(IEcsSystems systems)
        {
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