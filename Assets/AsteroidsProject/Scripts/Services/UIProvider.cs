using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite.Unity.Ugui;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.Services
{
    public class UIProvider : IUIProvider, IInitializable
    {
        private readonly IGameConfigProvider configProvider;
        private readonly IAssetProvider assetProvider;
        private List<IUIScreenController> controllers;
        private IUIScreenController loadingScreen;
        private EcsUguiEmitter uguiEmitter;

        public UIProvider(IGameConfigProvider configProvider, IAssetProvider assetProvider)
        {
            this.configProvider = configProvider;
            this.assetProvider = assetProvider;
        }

        public EcsUguiEmitter UIRoot
        {
            get => uguiEmitter;
            set => uguiEmitter = value;
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

        public async UniTask InitAllSceneUIByLabel(string label)
        {
            var objects = await assetProvider.LoadAllByLabelAsync<GameObject>(label);

            foreach (var obj in objects)
            {
                var screen = Object.Instantiate(obj, UIRoot.transform);
                var controller = screen.GetComponent<IUIScreenController>();
                controllers.Add(controller);
                controller.Hide();
            }
        }

        public void Dispose()
        {
            UIRoot = null;
            controllers.Clear();
        }

        private void InitLoadingScreen()
        {
            var prefab = assetProvider.ResourceLoad<GameObject>(configProvider.GameConfig.LoadingScreenPath);
            var go = Object.Instantiate(prefab);
            loadingScreen = go.GetComponent<IUIScreenController>();
            loadingScreen.DontDestroyOnLoad();
        }
    }
}