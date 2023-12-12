using AsteroidsProject.Shared;
using System;
using Zenject;

namespace AsteroidsProject.GameLogic.EntryPoint.GameScene
{
    public class GameSceneEntryPoint : IInitializable, ITickable, IFixedTickable, IDisposable
    {
        private readonly ILoadingScreenService loadingScreen;
        private readonly IEcsSystemsRunner ecsSystemsRunner;
        private readonly IAssetProvider assetProvider;
        private readonly IUIService uiService;

        public GameSceneEntryPoint(ILoadingScreenService loadingScreen, IEcsSystemsRunner ecsSystemsRunner, IAssetProvider assetProvider,
            IUIService uiService)
        {
            this.loadingScreen = loadingScreen;
            this.ecsSystemsRunner = ecsSystemsRunner;
            this.assetProvider = assetProvider;
            this.uiService = uiService;
        }

        public async void Initialize()
        {
            loadingScreen.Show();
            await assetProvider.PreLoadAsyncByLabel(AssetLabels.InGameScene.ToString());
            await uiService.PreLoadUI();
            ecsSystemsRunner.Initialize();
            uiService.Get<IPlayerShipStatsScreenPresenter>().Show();
            uiService.Get<IPlayerSecondaryWeaponScreenPresenter>().Show();
            loadingScreen.Hide();
        }

        public void Tick()
        {
            ecsSystemsRunner.Tick();
        }

        public void FixedTick()
        {
            ecsSystemsRunner.FixedTick();
        }

        public void Dispose()
        {
            ecsSystemsRunner.Dispose();
        }
    }
}
