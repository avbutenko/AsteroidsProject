using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.PlayerInput
{
    public class ClearPlayerInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
        }

        public void Run(IEcsSystems systems)
        {
            Clear<CAccelerationVector>();
            Clear<CRotationDirection>();
            Clear<CAttackRequest>();
        }

        private void Clear<T>() where T : struct
        {
            var filter = world.Filter<T>().End();
            var pool = world.GetPool<T>();

            foreach (var entity in filter)
            {
                pool.Del(entity);
            }
        }
    }
}