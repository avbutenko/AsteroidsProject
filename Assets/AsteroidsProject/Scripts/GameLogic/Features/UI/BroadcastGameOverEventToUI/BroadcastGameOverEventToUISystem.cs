using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.UI.BroadcastGameOverEventToUI
{
    public class BroadcastGameOverEventToUISystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IUIProvider uiProvider;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CGameOverEvent> eventPool;
        private IGameOverScreenPresenter gameOverScreen;
        private IPlayerShipStatsScreenPresenter shipStatsScreen;
        private IPlayerSecondaryWeaponScreenPresenter secondaryWeaponScreen;

        public BroadcastGameOverEventToUISystem(IUIProvider uiProvider)
        {
            this.uiProvider = uiProvider;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CGameOverEvent>().End();
            eventPool = world.GetPool<CGameOverEvent>();
            //gameOverScreen = uiProvider.Get<IGameOverScreenPresenter>();
            //shipStatsScreen = uiProvider.Get<IPlayerShipStatsScreenPresenter>();
            //secondaryWeaponScreen = uiProvider.Get<IPlayerSecondaryWeaponScreenPresenter>();
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

                if (secondaryWeaponScreen.IsVisible)
                {
                    secondaryWeaponScreen.Hide();
                }

                eventPool.Del(entity);
            }
        }
    }
}