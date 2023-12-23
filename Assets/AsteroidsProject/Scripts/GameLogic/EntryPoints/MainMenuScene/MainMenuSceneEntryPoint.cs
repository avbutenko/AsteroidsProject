using AsteroidsProject.Shared;
using System;
using Zenject;

namespace AsteroidsProject.GameLogic.EntryPoint.MainMenuScene
{
    public class MainMenuSceneEntryPoint : IInitializable, ITickable, IFixedTickable, IDisposable
    {
        private readonly IEcsSystemsRunner ecsSystemsRunner;
        private readonly IUIProvider uiProvider;
        private readonly IAssetProvider assetProvider;
        private readonly IGameConfigProvider configProvider;
        private readonly IConfigLoader configLoader;

        public MainMenuSceneEntryPoint(
            IEcsSystemsRunner ecsSystemsRunner,
            IAssetProvider assetProvider,
            IUIProvider uiProvider,
            IGameConfigProvider configProvider,
            IConfigLoader configLoader)
        {
            this.ecsSystemsRunner = ecsSystemsRunner;
            this.uiProvider = uiProvider;
            this.assetProvider = assetProvider;
            this.configProvider = configProvider;
            this.configLoader = configLoader;
        }

        public async void Initialize()
        {
            uiProvider.LoadingScreen.Show();
            var sceneConfig = await configLoader.Load<SceneConfig>(configProvider.GameConfig.ScenesConfig.MainMenuSceneConfigLabel);
            await assetProvider.PreLoadAllByLabelAsync(sceneConfig.PreLoadAssetLabel);
            await uiProvider.PreInitUIByLabel(sceneConfig.PreInitUILabel);
            ecsSystemsRunner.PreInitSystems(sceneConfig.EcsUpdateSystems, sceneConfig.EcsFixedUpdateSystems);
            ecsSystemsRunner.Initialize();
            uiProvider.Get<IMainMenuScreenController>().Show();
            uiProvider.LoadingScreen.Hide();
        }

        public void Tick()
        {
            ecsSystemsRunner.Tick();
        }

        public void FixedTick()
        {
            ecsSystemsRunner.Tick();
        }

        public void Dispose()
        {
            assetProvider.Dispose();
            configLoader.Dispose();
            ecsSystemsRunner.Dispose();
        }
    }
}