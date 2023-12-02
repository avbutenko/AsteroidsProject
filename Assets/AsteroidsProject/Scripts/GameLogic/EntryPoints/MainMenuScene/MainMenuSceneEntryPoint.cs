using AsteroidsProject.Shared;
using Zenject;

namespace AsteroidsProject.GameLogic.EntryPoint.MainMenuScene
{
    public class MainMenuSceneEntryPoint : IInitializable
    {
        private readonly ILoadingScreenService loadingScreen;
        private readonly IUIService uiService;
        private readonly IAssetProvider assetProvider;

        public MainMenuSceneEntryPoint(IAssetProvider assetProvider, ILoadingScreenService loadingScreen, IUIService uiService)
        {
            this.loadingScreen = loadingScreen;
            this.uiService = uiService;
            this.assetProvider = assetProvider;
        }

        public async void Initialize()
        {
            loadingScreen.Show();
            await assetProvider.PreLoadAsyncByLabel("InMainMenuScene");
            await uiService.PreLoadUI();
            uiService.Get<IMainMenuScreenPresenter>().Show();
            loadingScreen.Hide();
        }
    }
}