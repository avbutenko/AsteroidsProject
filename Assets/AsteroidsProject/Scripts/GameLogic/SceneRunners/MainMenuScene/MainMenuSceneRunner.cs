using AsteroidsProject.Shared;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace AsteroidsProject.GameLogic.SceneRunners.MainMenuScene
{
    public class MainMenuSceneRunner : IInitializable
    {
        private readonly IUIFactory uiFactory;
        private readonly IMainMenuSceneData sceneData;
        private readonly ISceneLoader sceneLoader;
        private readonly ILoadingScreen loadingScreen;

        public MainMenuSceneRunner(IUIFactory uiFactory, IMainMenuSceneData sceneData, ISceneLoader sceneLoader, ILoadingScreen loadingScreen)
        {
            this.uiFactory = uiFactory;
            this.sceneData = sceneData;
            this.sceneLoader = sceneLoader;
            this.loadingScreen = loadingScreen;
        }

        public async void Initialize()
        {
            loadingScreen.Show();
            var mainMenuView = await uiFactory.CreateAsync<IMainMenuView>("Bundles/Scenes/MainMenuScene/Prefabs/MainMenuView.prefab", sceneData.CanvasTransform);
            loadingScreen.Hide();
            mainMenuView.StartButton.onClick.AddListener(StartGame);
            mainMenuView.ExitButton.onClick.AddListener(ExitGame);
        }

        private async void StartGame()
        {
            //loadingScreen.Show();
            await sceneLoader.LoadSceneAsync("Bundles/Scenes/GameScene/GameScene.unity", LoadSceneMode.Single, true);
            //loadingScreen.Hide();
        }

        private void ExitGame()
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

}