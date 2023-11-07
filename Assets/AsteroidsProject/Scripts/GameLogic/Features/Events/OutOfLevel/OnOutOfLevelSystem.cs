using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events.OnOutOfLevel
{
    public class OnOutOfLevelSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<COutOfLevelEvent>().End();
            var eventPool = world.GetPool<COutOfLevelEvent>();
            var onOutOfLevelPool = world.GetPool<COnOutOfLevel>();

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