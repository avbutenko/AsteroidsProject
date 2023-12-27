using Leopotam.EcsLite.Unity.Ugui;
using Leopotam.EcsLite;
using AsteroidsProject.Shared;
using UnityEngine.Scripting;

namespace AsteroidsProject.GameLogic.Features.UI
{
    public class GamePauseScreenControlSystem : EcsUguiCallbackSystem, IEcsInitSystem
    {
        private readonly IUIProvider uiProvider;
        private readonly ITimeService timeService;
        private readonly ISceneLoader sceneLoader;
        private IGamePauseScreenController screenController;

        public GamePauseScreenControlSystem(IUIProvider uiProvider, ITimeService timeService, ISceneLoader sceneLoader)
        {
            this.uiProvider = uiProvider;
            this.timeService = timeService;
            this.sceneLoader = sceneLoader;
        }

        public void Init(IEcsSystems systems)
        {
            screenController = uiProvider.Get<IGamePauseScreenController>();
        }

        [Preserve]
        [EcsUguiClickEvent(Identifiers.Ui.ResumeButtonName)]
        private void OnClickResumeGame(in EcsUguiClickEvent e)
        {
            timeService.TooglePause();
            screenController.Hide();
        }

        [Preserve]
        [EcsUguiClickEvent(Identifiers.Ui.PauseScreenExitButtonName)]
        private void OnClickExitGame(in EcsUguiClickEvent e)
        {
            timeService.TooglePause();
            HandleExit();
        }

        private async void HandleExit()
        {
            screenController.Hide();
            uiProvider.LoadingScreen.Show();
            await sceneLoader.LoadSceneAsync(Identifiers.Scenes.MainMenuSceneName);
            uiProvider.LoadingScreen.Hide();
        }
    }
}