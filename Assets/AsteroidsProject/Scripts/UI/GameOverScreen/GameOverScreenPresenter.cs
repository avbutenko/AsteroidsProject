using AsteroidsProject.Shared;
using UnityEngine.SceneManagement;

namespace AsteroidsProject.UI.GameOverScreen
{
    public class GameOverScreenPresenter : IGameOverScreenPresenter
    {
        private readonly IGameOverScreenView view;
        private readonly ISceneLoader sceneLoader;
        private readonly ILoadingScreenService loadingScreen;
        private readonly IRestartService restartService;

        public GameOverScreenPresenter(IUIScreenView view, ISceneLoader sceneLoader,
            ILoadingScreenService loadingScreen, IRestartService restartService)
        {
            this.view = (IGameOverScreenView)view;
            this.view.RestartButton.onClick.AddListener(RestartButtonClick);
            this.view.ExitButton.onClick.AddListener(ExitButtonClick);
            this.sceneLoader = sceneLoader;
            this.loadingScreen = loadingScreen;
            this.restartService = restartService;
        }

        public void Hide()
        {
            view.Hide();
        }

        public void Show()
        {
            view.Show();
        }

        private async void ExitButtonClick()
        {
            view.Hide();
            loadingScreen.Show();
            view.RestartButton.onClick.RemoveAllListeners();
            view.ExitButton.onClick.RemoveAllListeners();
            await sceneLoader.LoadSceneAsync("MainMenuScene", LoadSceneMode.Single, false);
            loadingScreen.Hide();
        }

        private void RestartButtonClick()
        {
            view.Hide();
            restartService.Restart();
        }
    }
}