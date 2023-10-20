using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Destroy
{
    public class DestroySystem : IEcsRunSystem
    {
        private readonly IActiveGameObjectMapService activeGameObjectMapService;
        private readonly IGameObjectPool gameObjectPool;

        public DestroySystem(IActiveGameObjectMapService activeGameObjectMapService, IGameObjectPool gameObjectPool)
        {
            this.activeGameObjectMapService = activeGameObjectMapService;
            this.gameObjectPool = gameObjectPool;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CDestroyRequest>()
                              .Inc<CGameObject>()
                              .End();

            var linkToGameObjectPool = world.GetPool<CGameObject>();

            foreach (var entity in filter)
            {
                ref var link = ref linkToGameObjectPool.Get(entity).Link;
                var go = activeGameObjectMapService.GetValuePair(link).Go;

                if (link is IPoolable)
                {

                    gameObjectPool.Push(go);
                }
                else
                {
                    link.DestroySelf();
                }

                activeGameObjectMapService.Remove(link);
                world.DelEntity(entity);
            }
        }
    }
}