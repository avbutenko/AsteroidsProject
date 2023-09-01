using AsteroidsProject.GameLogic.Features.Common;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class UpdateViewPositionSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<GameplayObjectViewComponent>()
                              .Inc<Position>()
                              .End();

            var viewPool = world.GetPool<GameplayObjectViewComponent>();
            var positionPool = world.GetPool<Position>();


            foreach (var entity in filter)
            {
                ref var position = ref positionPool.Get(entity).Value;
                ref var view = ref viewPool.Get(entity).View;

                view.Transform.localPosition = position;
            }
        }
    }
}