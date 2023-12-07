using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.UI.BroadcastGameOverEventToUI
{
    public class BroadcastGameOverEventToUISystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IUIService uiService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CGameOverEvent> eventPool;
        private IGameOverScreenPresenter gameOverScreen;
        private IPlayerShipStatsScreenPresenter shipStatsScreen;

        public BroadcastGameOverEventToUISystem(IUIService uiService)
        {
            this.uiService = uiService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CGameOverEvent>().End();
            eventPool = world.GetPool<CGameOverEvent>();
            gameOverScreen = uiService.Get<IGameOverScreenPresenter>();
            shipStatsScreen = uiService.Get<IPlayerShipStatsScreenPresenter>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                if (!gameOverScreen.IsVisible)
                {
                    gameOverScreen.Show();
                }

                if (shipStatsScreen.IsVisible)
                {
                    shipStatsScreen.Hide();
                }

                eventPool.Del(entity);
            }
        }
    }
}