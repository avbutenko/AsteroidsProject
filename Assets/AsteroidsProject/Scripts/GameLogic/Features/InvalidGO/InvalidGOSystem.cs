using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.InvalidGO
{
    public class InvalidGOSystem : IEcsRunSystem
    {
        private readonly IActiveGOMappingService activeGOMappingService;
        private readonly IGameObjectPool gameObjectPool;

        public InvalidGOSystem(IActiveGOMappingService activeGOMappingService, IGameObjectPool gameObjectPool)
        {
            this.activeGOMappingService = activeGOMappingService;
            this.gameObjectPool = gameObjectPool;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CInvalidGameObjectInstanceID>().End();
            var goIDPool = world.GetPool<CInvalidGameObjectInstanceID>();

            foreach (var entity in filter)
            {
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
                world.DelEntity(entity);
            }
        }
    }
}