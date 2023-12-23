using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.UI.GameOverScreen
{
    public class GameOverScreenFactory : IUIScreenFactoryAsync
    {
        private readonly IAssetProvider assetProvider;
        private readonly IGameConfigProvider configProvider;
        private readonly DiContainer diContainer;

        public GameOverScreenFactory(DiContainer diContainer, IAssetProvider assetProvider, IGameConfigProvider configProvider)
        {
            this.diContainer = diContainer;
            this.configProvider = configProvider;
            this.assetProvider = assetProvider;
        }

        public async UniTask<IUIScreenPresenter> CreateAsync()
        {
            return default;
            //var prefab = await assetProvider.LoadAsync<GameObject>(configProvider.GameConfig.UIConfig.GameOverScreenPath);
            //var presenter = Object.Instantiate(prefab);
            //var model = new GameOverScreenModel();
            //var extraArgs = new List<object> { model };
            //diContainer.InjectGameObjectForComponent(presenter, typeof(GameOverScreenPresenter), extraArgs);
            //return presenter.GetComponent<IUIScreenPresenter>();
        }
    }
}