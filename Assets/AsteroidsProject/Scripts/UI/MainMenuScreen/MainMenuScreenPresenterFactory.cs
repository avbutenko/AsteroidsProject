using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.UI.MainMenuScreen
{
    public class MainMenuScreenPresenterFactory : IUIScreenPresenterFactoryAsync
    {
        private const string prefabAddress = "MainMenuScreen";
        private readonly IAssetProvider assetProvider;
        private readonly DiContainer diContainer;

        public MainMenuScreenPresenterFactory(DiContainer diContainer, IAssetProvider assetProvider)
        {
            this.diContainer = diContainer;
            this.assetProvider = assetProvider;
        }

        public async UniTask<IUIScreenPresenter> CreateAsync()
        {
            var prefab = await assetProvider.LoadAsync<GameObject>(prefabAddress);
            var presenter = Object.Instantiate(prefab);
            diContainer.InjectGameObject(presenter);
            return presenter.GetComponent<IUIScreenPresenter>();
        }
    }
}