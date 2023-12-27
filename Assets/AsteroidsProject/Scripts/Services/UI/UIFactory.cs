using AsteroidsProject.Shared;
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

        public IUIScreenController Create(string address)
        {
            var prefab = assetProvider.ResourceLoad<GameObject>(address);
            return Instantiate(prefab);
        }

        public IUIScreenController Instantiate(GameObject prefab)
        {
            var go = Object.Instantiate(prefab);
            return go.GetComponent<IUIScreenController>();
        }
    }
}