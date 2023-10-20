using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.UpdateGameObjectView
{
    public class UpdateGameObjectViewRotationSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CGameObject>()
                              .Inc<CRotation>()
                              .End();

            var viewPool = world.GetPool<CGameObject>();
            var rotationPool = world.GetPool<CRotation>();


            foreach (var entity in filter)
            {
                ref var rotation = ref rotationPool.Get(entity).Value;
                ref var view = ref viewPool.Get(entity).Link;

                view.Rotation = rotation;
            }
        }
    }
}