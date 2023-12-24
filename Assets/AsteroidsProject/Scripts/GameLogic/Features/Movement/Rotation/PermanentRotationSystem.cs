using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class PermanentRotationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CPermanentRotationDirection> permanentRotationPool;
        private EcsPool<CRotationDirection> rotationDirectionPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CPermanentRotationDirection>().End();
            permanentRotationPool = world.GetPool<CPermanentRotationDirection>();
            rotationDirectionPool = world.GetPool<CRotationDirection>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var direction = ref permanentRotationPool.Get(entity).Value;
                rotationDirectionPool.Add(entity).Value = direction;
            }
        }
    }
}