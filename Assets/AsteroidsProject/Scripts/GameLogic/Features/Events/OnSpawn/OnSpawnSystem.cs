using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events.OnSpawn
{
    public class OnSpawnSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CSpawnedEntityEvent>().End();
            var eventPool = world.GetPool<CSpawnedEntityEvent>();
            var onSpawnPool = world.GetPool<COnSpawn>();

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