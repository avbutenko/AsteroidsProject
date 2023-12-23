using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.UI.PlayerSecondaryWeaponScreen
{
    public class PlayerSecondaryWeaponScreenFactory : IUIScreenFactoryAsync
    {
        private readonly IAssetProvider assetProvider;
        private readonly IGameConfigProvider configProvider;
        private readonly DiContainer diContainer;

        public PlayerSecondaryWeaponScreenFactory(DiContainer diContainer, IAssetProvider assetProvider, IGameConfigProvider configProvider)
        {
            this.diContainer = diContainer;
            this.assetProvider = assetProvider;
            this.configProvider = configProvider;
        }

        public async UniTask<IUIScreenPresenter> CreateAsync()
        {
            return default;
            //var prefab = await assetProvider.LoadAsync<GameObject>(configProvider.GameConfig.UIConfig.PlayerSecondaryWeaponScreenPath);
            //var presenter = Object.Instantiate(prefab);
            //var model = new PlayerSecondaryWeaponScreenModel();
            //var extraArgs = new List<object> { model };
            //diContainer.InjectGameObjectForComponent(presenter, typeof(PlayerSecondaryWeaponScreenPresenter), extraArgs);
            //return presenter.GetComponent<IUIScreenPresenter>();
        }
    }
}