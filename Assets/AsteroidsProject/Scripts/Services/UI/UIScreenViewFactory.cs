using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class UIScreenViewFactory : IUIScreenViewFactory
    {
        private readonly IAssetProvider assetProvider;

        public UIScreenViewFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public async UniTask<IUIScreenView> CreateAsync(string prefabAddress)
        {
            var prefab = await assetProvider.LoadAsync<GameObject>(prefabAddress);
            var instance = Object.Instantiate(prefab);
            instance.SetActive(false);
            var view = instance.GetComponent<IUIScreenView>();
            return view;
        }
    }
}