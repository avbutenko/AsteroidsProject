using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events.OnOutOfLevel
{
    public class OnOutOfLevelSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<COutOfLevelEvent> eventPool;
        private EcsPool<COnOutOfLevel> onOutOfLevelPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<COutOfLevelEvent>().End();
            eventPool = world.GetPool<COutOfLevelEvent>();
            onOutOfLevelPool = world.GetPool<COnOutOfLevel>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var packedEntity = ref eventPool.Get(entity).PackedEntity;

                if (packedEntity.Unpack(world, out int outOfLevelEntity))
                {
                    if (onOutOfLevelPool.Has(outOfLevelEntity))
                    {
                        ref var components = ref onOutOfLevelPool.Get(outOfLevelEntity).AddToSelfComponents;
                        world.AddRawComponentsToEntity(outOfLevelEntity, components);
                    }
                }

                eventPool.Del(entity);
            }
        }
    }
}