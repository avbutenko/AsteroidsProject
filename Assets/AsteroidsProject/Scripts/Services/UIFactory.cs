using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider assetProvider;

        public UIFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public async UniTask<T> CreateAsync<T>(string prefabAddress)
        {
            var prefab = await assetProvider.LoadAsync<GameObject>(prefabAddress);
            var go = Object.Instantiate(prefab);
            var result = go.GetComponent<T>();
            return result;
        }

        public async UniTask<T> CreateAsync<T>(string prefabAddress, Transform parent)
        {
            var prefab = await assetProvider.LoadAsync<GameObject>(prefabAddress);
            var go = Object.Instantiate(prefab, parent);
            var result = go.GetComponent<T>();
            return result;
        }
    }
}