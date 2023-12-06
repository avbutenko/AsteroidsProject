using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.UI.GameOverScreen
{
    public class GameOverScreenPresenterFactory : IUIScreenPresenterFactoryAsync
    {
        private const string prefabAddress = "GameOverScreen";
        private readonly IAssetProvider assetProvider;
        private readonly DiContainer diContainer;

        public GameOverScreenPresenterFactory(DiContainer diContainer, IAssetProvider assetProvider)
        {
            this.diContainer = diContainer;
            this.assetProvider = assetProvider;
        }

        public async UniTask<IUIScreenPresenter> CreateAsync()
        {
            var prefab = await assetProvider.LoadAsync<GameObject>(prefabAddress);
            var presenter = Object.Instantiate(prefab);
            var model = new GameOverScreenModel();
            var extraArgs = new List<object> { model };
            diContainer.InjectGameObjectForComponent(presenter, typeof(GameOverScreenPresenter), extraArgs);
            return presenter.GetComponent<IUIScreenPresenter>();
        }
    }
}