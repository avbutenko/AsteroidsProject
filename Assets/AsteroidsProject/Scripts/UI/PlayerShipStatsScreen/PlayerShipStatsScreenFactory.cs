using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.UI.PlayerShipStatsScreen
{
    public class PlayerShipStatsScreenFactory : IUIScreenFactoryAsync
    {
        private readonly IAssetProvider assetProvider;
        private readonly IGameConfigProvider configProvider;
        private readonly DiContainer diContainer;

        public PlayerShipStatsScreenFactory(DiContainer diContainer, IAssetProvider assetProvider, IGameConfigProvider configProvider)
        {
            this.diContainer = diContainer;
            this.assetProvider = assetProvider;
            this.configProvider = configProvider;
        }

        public async UniTask<IUIScreenPresenter> CreateAsync()
        {
            return default;
            //var prefab = await assetProvider.LoadAsync<GameObject>(configProvider.GameConfig.UIConfig.PlayerShipStatsScreenPath);
            //var presenter = Object.Instantiate(prefab);
            //var model = new PlayerShipStatsScreenModel();
            //var extraArgs = new List<object> { model };
            //diContainer.InjectGameObjectForComponent(presenter, typeof(PlayerShipStatsScreenPresenter), extraArgs);
            //return presenter.GetComponent<IUIScreenPresenter>();
        }
    }
}