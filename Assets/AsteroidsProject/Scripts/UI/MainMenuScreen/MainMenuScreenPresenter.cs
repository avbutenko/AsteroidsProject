using AsteroidsProject.Shared;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace AsteroidsProject.UI.MainMenuScreen
{
    public class MainMenuScreenPresenter : IMainMenuScreenPresenter, IInitializable
    {
        private readonly IMainMenuScreenView view;
        private readonly ISceneLoader sceneLoader;
        private readonly ILoadingScreenService loadingScreen;

        public MainMenuScreenPresenter(IUIScreenView view, ISceneLoader sceneLoader, ILoadingScreenService loadingScreen)
        {
            this.view = (IMainMenuScreenView)view;
            this.sceneLoader = sceneLoader;
            this.loadingScreen = loadingScreen;
        }

        public void Initialize()
        {
            this.view.StartButton.onClick.AddListener(StartButtonClick);
            this.view.ExitButton.onClick.AddListener(ExitButtonClick);
        }

        public bool IsVisible => view.IsVisible;

        public void Hide()
        {
            view.Hide();
        }

        public void Show()
        {
            view.Show();
        }

        private async void StartButtonClick()
        {
            loadingScreen.Show();
            view.StartButton.onClick.RemoveAllListeners();
            view.ExitButton.onClick.RemoveAllListeners();
            await sceneLoader.LoadSceneAsync("GameScene", LoadSceneMode.Single, false);
            loadingScreen.Hide();
        }

        private void ExitButtonClick()
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}