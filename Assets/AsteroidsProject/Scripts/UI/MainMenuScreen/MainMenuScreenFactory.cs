using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.UI.MainMenuScreen
{
    public class MainMenuScreenFactory : IUIScreenFactoryAsync
    {
        private readonly IAssetProvider assetProvider;
        private readonly IGameConfigProvider configProvider;
        private readonly DiContainer diContainer;

        public MainMenuScreenFactory(DiContainer diContainer, IAssetProvider assetProvider, IGameConfigProvider configProvider)
        {
            this.diContainer = diContainer;
            this.assetProvider = assetProvider;
            this.configProvider = configProvider;
        }

        public async UniTask<IUIScreenPresenter> CreateAsync()
        {
            var prefab = await assetProvider.LoadAsync<GameObject>(configProvider.GameConfig.UIConfig.MainMenuScreenPath);
            var presenter = Object.Instantiate(prefab);
            diContainer.InjectGameObject(presenter);
            return presenter.GetComponent<IUIScreenPresenter>();
        }
    }
}