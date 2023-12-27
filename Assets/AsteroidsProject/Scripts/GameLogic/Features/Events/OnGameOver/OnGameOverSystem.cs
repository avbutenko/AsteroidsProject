using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events
{
    public class OnGameOverSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IUIProvider uiProvider;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CGameOverEvent> eventPool;
        private IGameOverScreenController gameOverScreen;
        private IPlayerShipStatsScreenController shipStatsScreen;
        private IPlayerShipSecondaryWeaponScreenController secondaryWeaponScreen;

        public OnGameOverSystem(IUIProvider uiProvider)
        {
            this.uiProvider = uiProvider;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CGameOverEvent>().End();
            eventPool = world.GetPool<CGameOverEvent>();
            gameOverScreen = uiProvider.Get<IGameOverScreenController>();
            shipStatsScreen = uiProvider.Get<IPlayerShipStatsScreenController>();
            secondaryWeaponScreen = uiProvider.Get<IPlayerShipSecondaryWeaponScreenController>();
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