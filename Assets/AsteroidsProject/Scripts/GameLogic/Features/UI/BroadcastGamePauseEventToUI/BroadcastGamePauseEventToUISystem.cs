using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.UI.BroadcastGamePauseEventToUI
{
    public class BroadcastGamePauseEventToUISystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IUIService uiService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CGamePauseEvent> eventPool;
        private IGamePauseScreenPresenter pauseScreen;

        public BroadcastGamePauseEventToUISystem(IUIService uiService)
        {
            this.uiService = uiService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CGamePauseEvent>().End();
            eventPool = world.GetPool<CGamePauseEvent>();
            pauseScreen = uiService.Get<IGamePauseScreenPresenter>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                if (!pauseScreen.IsVisible)
                {
                    pauseScreen.Show();
                }

                eventPool.Del(entity);
            }
        }
    }
}