using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.UpdateGameObjectView
{
    public class UpdateGameObjectViewPositionSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<LinkToGameObject>()
                              .Inc<Position>()
                              .End();

            var viewPool = world.GetPool<LinkToGameObject>();
            var positionPool = world.GetPool<Position>();


            foreach (var entity in filter)
            {
                ref var position = ref positionPool.Get(entity).Value;
                ref var view = ref viewPool.Get(entity).View;

                view.Position = position;
            }
        }
    }
}