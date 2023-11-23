using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Movement.FollowTarget
{
    public class SetFollowTargetSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CSetFollowTargetRequest> requestPool;
        private EcsPool<CFollow> followPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CSetFollowTargetRequest>().End();
            requestPool = world.GetPool<CSetFollowTargetRequest>();
            followPool = world.GetPool<CFollow>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var targetObject = ref requestPool.Get(entity).TargetComponent;
                var targetType = targetObject.GetType();
                var targetPool = world.GetPoolByType(targetType);

                int[] entities = null;
                world.GetAllEntities(ref entities);

                foreach (var processingEtity in entities)
                {
                    if (targetPool.Has(processingEtity))
                    {
                        followPool.Add(entity).Target = world.PackEntity(processingEtity);
                        requestPool.Del(entity);
                        return;
                    }
                }
            }
        }
    }
}