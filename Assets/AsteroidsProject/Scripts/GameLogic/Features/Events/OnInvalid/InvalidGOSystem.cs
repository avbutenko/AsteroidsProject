using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events
{
    public class InvalidGOSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IActiveGOMappingService activeGOMappingService;
        private readonly IGameObjectPool gameObjectPool;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CInvalidGameObjectInstanceID> goIDPool;

        public InvalidGOSystem(IActiveGOMappingService activeGOMappingService, IGameObjectPool gameObjectPool)
        {
            this.activeGOMappingService = activeGOMappingService;
            this.gameObjectPool = gameObjectPool;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CInvalidGameObjectInstanceID>().End();
            goIDPool = world.GetPool<CInvalidGameObjectInstanceID>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var goID = ref goIDPool.Get(entity).Value;

                if (!activeGOMappingService.TryGetGo(goID, out var go))
                {
                    world.DelEntity(entity);
                    continue;
                }

                if (!activeGOMappingService.TryGetGoLink(goID, out var goLink)) continue;

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