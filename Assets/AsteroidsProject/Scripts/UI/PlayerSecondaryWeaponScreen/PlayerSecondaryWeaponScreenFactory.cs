using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.UI.PlayerSecondaryWeaponScreen
{
    public class PlayerSecondaryWeaponScreenFactory : IUIScreenFactoryAsync
    {
        private const string prefabAddress = "PlayerSecondaryWeaponScreen";
        private readonly IAssetProvider assetProvider;
        private readonly DiContainer diContainer;

        public PlayerSecondaryWeaponScreenFactory(DiContainer diContainer, IAssetProvider assetProvider)
        {
            this.diContainer = diContainer;
            this.assetProvider = assetProvider;
        }

        public async UniTask<IUIScreenPresenter> CreateAsync()
        {
            var prefab = await assetProvider.LoadAsync<GameObject>(prefabAddress);
            var presenter = Object.Instantiate(prefab);
            var model = new PlayerSecondaryWeaponScreenModel();
            var extraArgs = new List<object> { model };
            diContainer.InjectGameObjectForComponent(presenter, typeof(PlayerSecondaryWeaponScreenPresenter), extraArgs);
            return presenter.GetComponent<IUIScreenPresenter>();
        }
    }
}
