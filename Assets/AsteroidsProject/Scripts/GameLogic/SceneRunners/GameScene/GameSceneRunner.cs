using AsteroidsProject.Shared;
using System;
using Zenject;

namespace AsteroidsProject.GameLogic.SceneRunners.GameScene
{
    public class GameSceneRunner : IInitializable, ITickable, IFixedTickable, IDisposable
    {
        private readonly ILoadingScreen loadingScreen;
        private readonly IEcsSystemsRunner ecsSystemsRunner;

        public GameSceneRunner(ILoadingScreen loadingScreen, IEcsSystemsRunner ecsSystemsRunner)
        {
            this.loadingScreen = loadingScreen;
            this.ecsSystemsRunner = ecsSystemsRunner;
        }

        public void Initialize()
        {
            //loadingScreen.Show();
            // 1) preload assets
            // 2) show HUD
            //loadingScreen.Hide();
            ecsSystemsRunner.Initialize();
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
