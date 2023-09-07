using AB_Utility.FromSceneToEntityConverter;
using AsteroidsProject.Infrastructure.Services;
using Leopotam.EcsLite;
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

        public async Task<GameObject> InstantiateAsync(string prefabAddress,
            Vector2 position, Quaternion rotation, Transform parentTransform, EcsWorld world)
        {
            var prefab = await assetProvider.Load<GameObject>(prefabAddress);
            var insinstance = EcsConverter.InstantiateAndCreateEntity(prefab, position, rotation, parentTransform, world);
            return insinstance;
        }
    }
}