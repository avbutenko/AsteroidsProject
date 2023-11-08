using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Score
{
    public class ScoreSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var reqestFilter = world.Filter<CCollectScoreRequest>().End();
            var scoreFilter = world.Filter<CScore>().End();

            var scorePool = world.GetPool<CScore>();
            var requestPool = world.GetPool<CCollectScoreRequest>();

            foreach (var requestEntity in reqestFilter)
            {
                ref var deltaValue = ref requestPool.Get(requestEntity).Value;

                foreach (var scoreHolderEntity in scoreFilter)
                {
                    ref var currentScore = ref scorePool.Get(scoreHolderEntity).Value;
                    currentScore += deltaValue;
                }

                requestPool.Del(requestEntity);
            }
        }
    }
}