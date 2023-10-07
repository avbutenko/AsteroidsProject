using AB_Utility.FromSceneToEntityConverter;
using AsteroidsProject.Shared;
using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class GameplayObjectViewFactory : IGameObjectFactory
    {
        private readonly IAssetProvider assetProvider;
        private readonly IPool pool;

        public GameplayObjectViewFactory(IAssetProvider assetProvider, IPool pool)
        {
            this.assetProvider = assetProvider;
            this.pool = pool;
        }

        public async Task<EntityWithGameObject> InstantiateAsync(SpawnInfo spawnInfo)
        {
            var prefab = await assetProvider.Load<GameObject>(spawnInfo.PrefabAddress);

            var gameObject = EcsConverter.InstantiateAndCreateEntity(prefab, spawnInfo.Position, spawnInfo.Rotation,
                spawnInfo.Parent, spawnInfo.World, out int entity);

            return new EntityWithGameObject { Entity = entity, GameObject = gameObject };
        }

        public async Task<GameObject> CreateAsync(SpawnInfo spawnInfo)
        {
            var prefab = await assetProvider.Load<GameObject>(spawnInfo.PrefabAddress);

            var isPoolable = prefab.TryGetComponent(out IPoolable _);

            if (isPoolable)
            {
                return GetPoolable(prefab, spawnInfo);
            }
            else
            {
                return Create(prefab, spawnInfo);
            }
        }

        private GameObject GetPoolable(GameObject prefab, SpawnInfo spawnInfo)
        {
            if (pool.HasObjects(prefab))
            {
                return PullFromPool(prefab, spawnInfo);
            }
            else
            {
                return CreatePoolable(prefab, spawnInfo);
            }
        }

        private GameObject PullFromPool(GameObject prefab, SpawnInfo spawnInfo)
        {
            var go = pool.Pull(prefab);
            SetGOParams(go, spawnInfo);
            return go;
        }

        private GameObject CreatePoolable(GameObject prefab, SpawnInfo spawnInfo)
        {
            var go = Create(prefab, spawnInfo);
            pool.Register(prefab, go);
            return go;
        }

        private GameObject Create(GameObject prefab, SpawnInfo spawnInfo)
        {
            var go = Object.Instantiate(prefab, spawnInfo.Position, spawnInfo.Rotation, spawnInfo.Parent);
            SetGOParams(go, spawnInfo);
            return go;
        }

        private void SetGOParams(GameObject go, SpawnInfo spawnInfo)
        {
            go.transform.position = spawnInfo.Position;
            go.transform.rotation = spawnInfo.Rotation;
            go.transform.parent = spawnInfo.Parent;
        }
    }
}