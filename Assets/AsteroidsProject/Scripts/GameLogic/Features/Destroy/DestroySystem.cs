using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Destroy
{
    public class DestroySystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<DestroyTag>()
                              .Inc<CLinkToGameObject>()
                              .End();

            var linkToGameObjectPool = world.GetPool<CLinkToGameObject>();

            foreach (var entity in filter)
            {
                ref var view = ref linkToGameObjectPool.Get(entity).View;
                view.DestroySelf();
                world.DelEntity(entity);
            }
        }
    }
}