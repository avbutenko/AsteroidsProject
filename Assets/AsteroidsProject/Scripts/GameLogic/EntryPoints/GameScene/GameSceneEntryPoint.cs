using AsteroidsProject.Shared;
using System;
using Zenject;

namespace AsteroidsProject.GameLogic.EntryPoint.GameScene
{
    public class GameSceneEntryPoint : IInitializable, ITickable, IFixedTickable, IDisposable
    {
        private readonly IEcsSystemsRunner ecsSystemsRunner;
        private readonly IAssetProvider assetProvider;
        private readonly IUIProvider uiProvider;

        public GameSceneEntryPoint(IEcsSystemsRunner ecsSystemsRunner, IAssetProvider assetProvider, IUIProvider uiProvider)
        {
            this.ecsSystemsRunner = ecsSystemsRunner;
            this.assetProvider = assetProvider;
            this.uiProvider = uiProvider;
        }

        public async void Initialize()
        {
            uiProvider.LoadingScreen.Show();
            await assetProvider.PreLoadAllByLabelAsync(AssetLabels.InGameScene.ToString());
            await uiProvider.PreInitUIByLabel(AssetLabels.InGameSceneUI.ToString());
            //ecsSystemsRunner.Initialize();
            uiProvider.LoadingScreen.Hide();
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
