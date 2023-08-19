using AsteroidsProject.Infrastructure.Services;
using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.EngineRelated.Services
{
    public class GameplayObjectViewFactory : IGameplayObjectViewFactory
    {
        private readonly IAssetProvider assetProvider;

        public GameplayObjectViewFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public async Task<GameObject> InstantiateAsync(string prefabAddress, Vector3 position, Quaternion rotation, Transform parentTransform)
        {
            var prefab = await assetProvider.Load<GameObject>(prefabAddress);
            var insinstance = Object.Instantiate(prefab, position, rotation, parentTransform);
            return insinstance;
        }
    }
}