using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;

namespace AsteroidsProject.GameLogic.Features.Events
{
    public class OnGameRestartSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IEcsSystemsRunner systemsRunner;
        private EcsWorld world;
        private EcsFilter restartFilter;
        private EcsFilter activeGOfilter;
        private EcsFilter invalidGOfilter;
        private EcsPool<CGameRestartEvent> restartPool;
        private EcsPool<CGameObjectInstanceID> goPool;

        public OnGameRestartSystem(IEcsSystemsRunner systemsRunner)
        {
            this.systemsRunner = systemsRunner;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            restartFilter = world.Filter<CGameRestartEvent>().End();
            activeGOfilter = world.Filter<CGameObjectInstanceID>().End();
            invalidGOfilter = world.Filter<CInvalidGameObjectInstanceID>().End();
            restartPool = world.GetPool<CGameRestartEvent>();
            goPool = world.GetPool<CGameObjectInstanceID>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var restartEntity in restartFilter)
            {
                foreach (var entity in activeGOfilter)
                {
                    world.NewEntityWith(new CInvalidGameObjectInstanceID { Value = goPool.Get(entity).Value });
                }
                restartPool.Del(restartEntity);
                Restart();
            }
        }

        private async void Restart()
        {
            await UniTask.WaitUntil(() => invalidGOfilter.GetEntitiesCount() == 0);
            systemsRunner.Restart();
        }
    }
}