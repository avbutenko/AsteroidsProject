using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.UpdateGameplayObjectView
{
    public class UpdateGameplayObjectViewRotationSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<LinkToGameplayObjectView>()
                              .Inc<Rotation>()
                              .End();

            var viewPool = world.GetPool<LinkToGameplayObjectView>();
            var rotationPool = world.GetPool<Rotation>();


            foreach (var entity in filter)
            {
                ref var rotation = ref rotationPool.Get(entity).Value;
                ref var view = ref viewPool.Get(entity).View;

                view.Rotation = rotation;
            }
        }
    }
}