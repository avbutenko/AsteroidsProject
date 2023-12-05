using AsteroidsProject.Shared;
using UnityEngine.SceneManagement;
using Zenject;

namespace AsteroidsProject.UI.GamePauseScreen
{
    public class GamePauseScreenPresenter : IGamePauseScreenPresenter, IInitializable
    {
        private readonly IGamePauseScreenView view;
        private readonly ITimeService timeService;
        private readonly ISceneLoader sceneLoader;
        private readonly ILoadingScreenService loadingScreen;

        public GamePauseScreenPresenter(IUIScreenView view, ITimeService timeService, ISceneLoader sceneLoader,
            ILoadingScreenService loadingScreen)
        {
            this.view = (IGamePauseScreenView)view;
            this.timeService = timeService;
            this.sceneLoader = sceneLoader;
            this.loadingScreen = loadingScreen;
        }

        public void Initialize()
        {
            this.view.ResumeButton.onClick.AddListener(ResumeButtonClick);
            this.view.ExitButton.onClick.AddListener(ExitButtonClick);
        }

        public bool IsVisible => view.IsVisible;

        public void Hide()
        {
            timeService.TooglePause();
            view.Hide();
        }

        public void Show()
        {
            timeService.TooglePause();
            view.Show();
        }

        private void ResumeButtonClick()
        {
            Hide();
        }

        private async void ExitButtonClick()
        {
            view.Hide();
            loadingScreen.Show();
            view.ResumeButton.onClick.RemoveAllListeners();
            view.ExitButton.onClick.RemoveAllListeners();
            await sceneLoader.LoadSceneAsync("MainMenuScene", LoadSceneMode.Single, false);
            loadingScreen.Hide();
        }
    }
}