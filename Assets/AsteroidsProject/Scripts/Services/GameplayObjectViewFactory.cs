using AB_Utility.FromSceneToEntityConverter;
using AsteroidsProject.Shared;
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

        public async Task<EntityWithGameObject> InstantiateAsync(SpawnInfo spawnInfo)
        {
            var prefab = await assetProvider.Load<GameObject>(spawnInfo.PrefabAddress);

            var gameObject = EcsConverter.InstantiateAndCreateEntity(prefab, spawnInfo.Position, spawnInfo.Rotation,
                spawnInfo.Parent, spawnInfo.World, out int entity);

            return new EntityWithGameObject { Entity = entity, GameObject = gameObject };
        }

        public async Task<GameObject> InstantiateGameObjectAsync(SpawnInfo spawnInfo)
        {
            var prefab = await assetProvider.Load<GameObject>(spawnInfo.PrefabAddress);
            return Object.Instantiate(prefab, spawnInfo.Position, spawnInfo.Rotation, spawnInfo.Parent);
        }
    }
}