using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Movement.FollowTarget
{
    public class SetFollowTargetSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CSetFollowTargetRequest>().End();
            var requestPool = world.GetPool<CSetFollowTargetRequest>();
            var followPool = world.GetPool<CFollow>();

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