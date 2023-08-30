using AsteroidsProject.GameLogic.Features.Common;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class UpdateViewRotationSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<GameplayObjectViewComponent>()
                              .Inc<Rotation.Rotation>()
                              .End();

            var viewPool = world.GetPool<GameplayObjectViewComponent>();
            var rotationPool = world.GetPool<Rotation.Rotation>();


            foreach (var entity in filter)
            {
                ref var rotation = ref rotationPool.Get(entity).Value;
                ref var view = ref viewPool.Get(entity).View;

                view.Transform.rotation = rotation;
            }
        }
    }
}