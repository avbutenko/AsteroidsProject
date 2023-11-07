using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events.OnDeath
{
    public class OnDeathSystem : IEcsRunSystem
    {
        private readonly IActiveGOMappingService activeGOMappingService;
        private readonly IGameObjectPool gameObjectPool;

        public OnDeathSystem(IActiveGOMappingService activeGOMappingService, IGameObjectPool gameObjectPool)
        {
            this.activeGOMappingService = activeGOMappingService;
            this.gameObjectPool = gameObjectPool;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CDeathEvent>().End();
            var eventPool = world.GetPool<CDeathEvent>();

            foreach (var entity in filter)
            {
                ref var packedEntity = ref eventPool.Get(entity).PackedEntity;

                if (packedEntity.Unpack(world, out int deadEntity) && deadEntity != 0) // just for testing make player immortal
                {
                    HandleOnDeathEvents(world, deadEntity);
                    HandleGO(world, deadEntity);
                    world.DelEntity(deadEntity);
                }

                eventPool.Del(entity);
            }
        }

        private void HandleOnDeathEvents(EcsWorld world, int entity)
        {
            var onDeathPool = world.GetPool<COnDeath>();

            if (onDeathPool.Has(entity))
            {
                ref var createList = ref onDeathPool.Get(entity).CreateEntities;

                foreach (var item in createList)
                {
                    world.NewEntityWithRawComponents(item.Components);
                }
            }
        }

        private void HandleGO(EcsWorld world, int entity)
        {
            var goIDPool = world.GetPool<CGameObjectInstanceID>();
            ref var goID = ref goIDPool.Get(entity).Value;
            if (!activeGOMappingService.TryGetGo(goID, out var go)) return;
            if (!activeGOMappingService.TryGetGoLink(goID, out var goLink)) return;

            if (goLink is IPoolable)
            {
                gameObjectPool.Push(go);
            }
            else
            {
                goLink.Destroy();
            }

            activeGOMappingService.Remove(goID);
        }
    }
}
