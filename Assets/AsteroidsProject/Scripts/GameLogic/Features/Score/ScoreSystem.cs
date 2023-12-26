using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Score
{
    public class ScoreSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter reqestFilter;
        private EcsFilter scoreFilter;
        private EcsPool<CScore> scorePool;
        private EcsPool<CCollectScoreRequest> requestPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            reqestFilter = world.Filter<CCollectScoreRequest>().End();
            scoreFilter = world.Filter<CScore>().End();
            scorePool = world.GetPool<CScore>();
            requestPool = world.GetPool<CCollectScoreRequest>();
            world.NewEntityWith<CScore>();
        }

        public void Run(IEcsSystems systems)
        {
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