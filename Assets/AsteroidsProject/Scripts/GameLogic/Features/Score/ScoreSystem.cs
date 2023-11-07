using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Score
{
    public class ScoreSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var filter = world.Filter<CCollectScoreRequest>()
                              .Inc<CScore>()
                              .End();

            var scorePool = world.GetPool<CScore>();
            var requestPool = world.GetPool<CCollectScoreRequest>();

            foreach (var entity in filter)
            {
                ref var currentScore = ref scorePool.Get(entity).Value;
                ref var deltaValue = ref requestPool.Get(entity).Value;
                currentScore += deltaValue;
            }
        }
    }
}