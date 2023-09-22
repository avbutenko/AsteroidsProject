using AB_Utility.FromSceneToEntityConverter;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class GameplayObjectViewFactory : IGameObjectFactory
    {
        private readonly IAssetProvider assetProvider;

        public GameplayObjectViewFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public async Task<EntityWithGameObject> InstantiateAsync(string prefabAddress,
            Vector2 position, Quaternion rotation, Transform parentTransform, EcsWorld world)
        {
            var prefab = await assetProvider.Load<GameObject>(prefabAddress);

            var gameObject = EcsConverter.InstantiateAndCreateEntity(prefab, position, rotation, parentTransform, world, out int entity);

            return new EntityWithGameObject { Entity = entity, GameObject = gameObject };
        }
    }
}