using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.Services
{
    public class UIProvider : IUIProvider, IInitializable
    {
        private readonly IGameConfigProvider configProvider;
        private readonly IUIFactory factory;
        private readonly IAssetProvider assetProvider;
        private List<IUIScreenController> controllers;
        private IUIScreenController loadingScreen;

        public UIProvider(IGameConfigProvider configProvider, IUIFactory factory, IAssetProvider assetProvider)
        {
            this.configProvider = configProvider;
            this.factory = factory;
            this.assetProvider = assetProvider;
        }

        public void Initialize()
        {
            controllers = new();
            InitLoadingScreen();
        }

        public IUIScreenController LoadingScreen => loadingScreen;

        public T Get<T>() where T : IUIScreenController
        {
            return (T)controllers.Find(x => x is T);
        }

        public async UniTask PreInitUIByLabel(string label)
        {
            var objects = await assetProvider.LoadByLabelAsync<GameObject>(label);

            foreach (var obj in objects)
            {
                var instance = factory.Instantiate(obj);
                controllers.Add(instance);
                instance.Hide();
            }
        }

        public void Dispose()
        {
            controllers.Clear();
        }

        private void InitLoadingScreen()
        {
            loadingScreen = factory.Create(configProvider.GameConfig.LoadingScreenPath);
            loadingScreen.DontDestroyOnLoad();
        }
    }
}