using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Randomization.Position
{
    public class RandomizePositionSystem : IEcsRunSystem
    {
        private readonly ILevelService level;
        public RandomizePositionSystem(ILevelService level)
        {

            this.level = level;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CRandomizePositionRequest>().End();

            var requestPool = world.GetPool<CRandomizePositionRequest>();
            var positionPool = world.GetPool<CPosition>();

            foreach (var entity in filter)
            {
                positionPool.Add(entity).Value = level.GetRandomPosition();
                requestPool.Del(entity);
            }
        }
    }
}