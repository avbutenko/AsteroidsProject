using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.UI.PlayerShipStatsScreen
{
    public class PlayerShipStatsScreenFactory : IUIScreenFactoryAsync
    {
        private const string prefabAddress = "PlayerShipStatsScreen";
        private readonly IAssetProvider assetProvider;
        private readonly DiContainer diContainer;

        public PlayerShipStatsScreenFactory(DiContainer diContainer, IAssetProvider assetProvider)
        {
            this.diContainer = diContainer;
            this.assetProvider = assetProvider;
        }

        public async UniTask<IUIScreenPresenter> CreateAsync()
        {
            var prefab = await assetProvider.LoadAsync<GameObject>(prefabAddress);
            var presenter = Object.Instantiate(prefab);
            var model = new PlayerShipStatsScreenModel();
            var extraArgs = new List<object> { model };
            diContainer.InjectGameObjectForComponent(presenter, typeof(PlayerShipStatsScreenPresenter), extraArgs);
            return presenter.GetComponent<IUIScreenPresenter>();
        }
    }
}
