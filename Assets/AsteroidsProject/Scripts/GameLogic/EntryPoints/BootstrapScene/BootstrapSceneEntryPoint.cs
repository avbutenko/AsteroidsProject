using AsteroidsProject.Shared;
using UnityEngine.SceneManagement;
using Zenject;

namespace AsteroidsProject.GameLogic.EntryPoint.BootstrapScene
{
    public class BootstrapSceneEntryPoint : IInitializable
    {
        private readonly ISceneLoader sceneLoader;
        private readonly ILoadingScreenService loadingScreen;

        public BootstrapSceneEntryPoint(ISceneLoader sceneLoader, ILoadingScreenService loadingScreen)
        {
            this.sceneLoader = sceneLoader;
            this.loadingScreen = loadingScreen;
        }

        public async void Initialize()
        {
            loadingScreen.Show();
            await sceneLoader.LoadSceneAsync("MainMenuScene", LoadSceneMode.Single, false);
            loadingScreen.Hide();
        }
    }
}