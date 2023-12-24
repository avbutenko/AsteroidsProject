using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events
{
    public class OnSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CSpawnedEntityEvent> eventPool;
        private EcsPool<COnSpawn> onSpawnPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CSpawnedEntityEvent>().End();
            eventPool = world.GetPool<CSpawnedEntityEvent>();
            onSpawnPool = world.GetPool<COnSpawn>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var packedEntity = ref eventPool.Get(entity).PackedEntity;

                if (packedEntity.Unpack(world, out int spawnedEntity))
                {
                    if (onSpawnPool.Has(spawnedEntity))
                    {
                        ref var components = ref onSpawnPool.Get(spawnedEntity).AddToSelfComponents;
                        world.AddRawComponentsToEntity(spawnedEntity, components);
                    }
                }

                eventPool.Del(entity);
            }
        }
    }
}