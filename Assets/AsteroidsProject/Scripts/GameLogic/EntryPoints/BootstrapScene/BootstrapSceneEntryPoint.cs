using AsteroidsProject.Shared;
using UnityEngine.SceneManagement;
using Zenject;

namespace AsteroidsProject.GameLogic.EntryPoint.BootstrapScene
{
    public class BootstrapSceneEntryPoint : IInitializable
    {
        private readonly ISceneLoader sceneLoader;
        private readonly ILoadingScreenService loadingScreen;
        private readonly IGameConfigProvider configProvider;

        public BootstrapSceneEntryPoint(ISceneLoader sceneLoader, ILoadingScreenService loadingScreen, IGameConfigProvider configProvider)
        {
            this.sceneLoader = sceneLoader;
            this.loadingScreen = loadingScreen;
            this.configProvider = configProvider;
        }

        public async void Initialize()
        {
            loadingScreen.Show();
            await sceneLoader.LoadSceneAsync(configProvider.GameConfig.ScenesConfig.FirstScenePath, LoadSceneMode.Single, false);
            loadingScreen.Hide();
        }
    }
}