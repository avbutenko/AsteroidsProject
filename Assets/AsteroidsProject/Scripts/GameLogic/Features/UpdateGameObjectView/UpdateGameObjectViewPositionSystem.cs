using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.UpdateGameObjectView
{
    public class UpdateGameObjectViewPositionSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CGameObject>()
                              .Inc<CPosition>()
                              .End();

            var viewPool = world.GetPool<CGameObject>();
            var positionPool = world.GetPool<CPosition>();


            foreach (var entity in filter)
            {
                ref var position = ref positionPool.Get(entity).Value;
                ref var view = ref viewPool.Get(entity).Link;

                view.Position = position;
            }
        }
    }
}