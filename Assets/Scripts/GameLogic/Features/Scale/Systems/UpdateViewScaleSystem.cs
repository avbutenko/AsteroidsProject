using AsteroidsProject.GameLogic.Features.Common;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Scale
{
    public class UpdateViewScaleSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<GameplayObjectViewComponent>()
                              .Inc<Scale>()
                              .End();

            var viewPool = world.GetPool<GameplayObjectViewComponent>();
            var scalePool = world.GetPool<Scale>();

            foreach (var entity in filter)
            {
                ref var scale = ref scalePool.Get(entity).Value;
                ref var view = ref viewPool.Get(entity).View;

                view.Transform.localScale = new Vector3(scale, scale, scale);
            }
        }
    }
}