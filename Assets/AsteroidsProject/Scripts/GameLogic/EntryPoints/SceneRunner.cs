﻿using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using System;
using Zenject;

namespace AsteroidsProject.GameLogic.EntryPoints
{
    public abstract class SceneRunner : IInitializable, ITickable, IFixedTickable, IDisposable
    {
        protected readonly IEcsSystemsRunner ecsSystemsRunner;
        protected readonly IUIProvider uiProvider;
        protected readonly IAssetProvider assetProvider;
        protected readonly IGameConfigProvider configProvider;
        protected readonly IConfigLoader configLoader;

        public SceneRunner(
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
            var sceneConfig = await LoadSceneConfigAsync();
            await assetProvider.PreLoadAllByLabelAsync(sceneConfig.PreLoadAssetLabel);
            await uiProvider.PreInitUIByLabel(sceneConfig.PreInitUILabel);
            ecsSystemsRunner.PreInitSystems(sceneConfig.EcsUpdateSystems, sceneConfig.EcsFixedUpdateSystems, sceneConfig.EcsGUISystems);
            ecsSystemsRunner.Initialize();
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
            uiProvider.Dispose();
            assetProvider.Dispose();
            configLoader.Dispose();
            ecsSystemsRunner.Dispose();
        }

        protected abstract UniTask<SceneConfig> LoadSceneConfigAsync();
    }
}