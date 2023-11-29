using AsteroidsProject.Shared;
using System;
using Zenject;

namespace AsteroidsProject.GameLogic.SceneRunners.GameScene
{
    public class GameSceneRunner : IInitializable, ITickable, IFixedTickable, IDisposable
    {
        private readonly ILoadingScreen loadingScreen;
        private readonly IEcsSystemsRunner ecsSystemsRunner;
        private readonly IAssetProvider assetProvider;

        public GameSceneRunner(ILoadingScreen loadingScreen, IEcsSystemsRunner ecsSystemsRunner, IAssetProvider assetProvider)
        {
            this.loadingScreen = loadingScreen;
            this.ecsSystemsRunner = ecsSystemsRunner;
            this.assetProvider = assetProvider;
        }

        public async void Initialize()
        {
            loadingScreen.Show();
            await assetProvider.PreLoadAsyncByLabel("InGameScene");
            ecsSystemsRunner.Initialize();
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
