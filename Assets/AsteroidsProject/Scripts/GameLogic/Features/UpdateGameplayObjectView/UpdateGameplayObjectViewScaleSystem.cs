using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.UpdateGameplayObjectView
{
    public class UpdateGameplayObjectViewScaleSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<LinkToGameplayObjectView>()
                              .Inc<Scale>()
                              .End();

            var viewPool = world.GetPool<LinkToGameplayObjectView>();
            var scalePool = world.GetPool<Scale>();

            foreach (var entity in filter)
            {
                ref var scale = ref scalePool.Get(entity).Value;
                ref var view = ref viewPool.Get(entity).View;

                view.Scale = scale;
            }
        }
    }
}