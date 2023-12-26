using AsteroidsProject.Shared;
using Zenject;

namespace AsteroidsProject.GameLogic.EntryPoints
{
    public class BootstrapSceneEntryPoint : IInitializable
    {
        private readonly ISceneLoader sceneLoader;
        private readonly IUIProvider uiProvider;
        private readonly IGameConfigProvider configProvider;

        public BootstrapSceneEntryPoint(ISceneLoader sceneLoader, IUIProvider uiProvider, IGameConfigProvider configProvider)
        {
            this.sceneLoader = sceneLoader;
            this.uiProvider = uiProvider;
            this.configProvider = configProvider;
        }

        public async void Initialize()
        {
            uiProvider.LoadingScreen.Show();
            await sceneLoader.LoadSceneAsync(configProvider.GameConfig.FirstSceneLabel);
            uiProvider.LoadingScreen.Hide();
        }
    }
}