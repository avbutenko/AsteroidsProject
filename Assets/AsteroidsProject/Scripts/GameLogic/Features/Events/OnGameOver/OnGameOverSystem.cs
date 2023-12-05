using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events.OnGameOver
{
    public class OnGameOverSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IUIService uiService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CGameOverEvent> eventPool;
        private IGameOverScreenPresenter gameOverScreen;

        public OnGameOverSystem(IUIService uiService)
        {
            this.uiService = uiService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CGameOverEvent>().End();
            eventPool = world.GetPool<CGameOverEvent>();
            gameOverScreen = uiService.Get<IGameOverScreenPresenter>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                if (!gameOverScreen.IsVisible)
                {
                    gameOverScreen.Show();
                }

                eventPool.Del(entity);
            }
        }
    }
}