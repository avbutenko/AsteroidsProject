using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events
{
    public class OnGamePauseSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IUIProvider uiProvider;
        private readonly ITimeService timeService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CGamePauseEvent> eventPool;
        private IGamePauseScreenController pauseScreen;

        public OnGamePauseSystem(IUIProvider uiProvider, ITimeService timeService)
        {
            this.uiProvider = uiProvider;
            this.timeService = timeService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CGamePauseEvent>().End();
            eventPool = world.GetPool<CGamePauseEvent>();
            pauseScreen = uiProvider.Get<IGamePauseScreenController>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                if (!pauseScreen.IsVisible)
                {
                    timeService.TooglePause();
                    pauseScreen.Show();
                }

                eventPool.Del(entity);
            }
        }
    }
}