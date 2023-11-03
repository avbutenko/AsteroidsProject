using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Destruction
{
    public class DestructionSystem : IEcsRunSystem
    {
        private readonly IActiveGOMappingService activeGOMappingService;
        private readonly IGameObjectPool gameObjectPool;

        public DestructionSystem(IActiveGOMappingService activeGOMappingService, IGameObjectPool gameObjectPool)
        {
            this.activeGOMappingService = activeGOMappingService;
            this.gameObjectPool = gameObjectPool;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CDestructionRequest>()
                              .Inc<CGameObjectInstanceID>()
                              .End();

            var goIDPool = world.GetPool<CGameObjectInstanceID>();

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
                    goLink.DestroySelf();
                }

                activeGOMappingService.Remove(goID);
                world.DelEntity(entity);
            }
        }
    }
}