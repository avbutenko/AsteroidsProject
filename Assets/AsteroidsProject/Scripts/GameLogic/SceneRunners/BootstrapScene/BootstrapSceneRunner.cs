using AsteroidsProject.Shared;
using UnityEngine.SceneManagement;
using Zenject;

namespace AsteroidsProject.GameLogic.SceneRunners.BootstrapScene
{
    public class BootstrapSceneRunner : IInitializable
    {
        private readonly ISceneLoader sceneLoader;
        private readonly ILoadingScreen loadingScreen;

        public BootstrapSceneRunner(ISceneLoader sceneLoader, ILoadingScreen loadingScreen)
        {
            this.sceneLoader = sceneLoader;
            this.loadingScreen = loadingScreen;
        }

        public async void Initialize()
        {
            loadingScreen.Show();
            await sceneLoader.LoadSceneAsync("Bundles/Scenes/MainMenuScene/MainMenuScene.unity", LoadSceneMode.Single, false);
            loadingScreen.Hide();
        }
    }
}