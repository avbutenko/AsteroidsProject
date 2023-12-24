using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.UI
{
    public class BroadcastGamePauseEventToUISystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IUIProvider uiProvider;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CGamePauseEvent> eventPool;
        //private IGamePauseScreenPresenter pauseScreen;

        public BroadcastGamePauseEventToUISystem(IUIProvider uiProvider)
        {
            this.uiProvider = uiProvider;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CGamePauseEvent>().End();
            eventPool = world.GetPool<CGamePauseEvent>();
            //pauseScreen = uiProvider.Get<IGamePauseScreenPresenter>();
        }

        public void Run(IEcsSystems systems)
        {
            //foreach (var entity in filter)
            //{
            //    if (!pauseScreen.IsVisible)
            //    {
            //        pauseScreen.Show();
            //    }

            //    eventPool.Del(entity);
            //}
        }
    }
}