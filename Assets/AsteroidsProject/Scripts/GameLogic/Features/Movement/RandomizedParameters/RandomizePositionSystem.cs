using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Randomization.Position
{
    public class RandomizePositionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ILevelService level;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CRandomizePositionRequest> requestPool;
        private EcsPool<CPosition> positionPool;

        public RandomizePositionSystem(ILevelService level)
        {
            this.level = level;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CRandomizePositionRequest>().End();
            requestPool = world.GetPool<CRandomizePositionRequest>();
            positionPool = world.GetPool<CPosition>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                positionPool.Add(entity).Value = level.GetRandomPosition();
                requestPool.Del(entity);
            }
        }
    }
}