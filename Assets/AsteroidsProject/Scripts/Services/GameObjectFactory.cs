using AsteroidsProject.Shared;
using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class GameObjectFactory : IGameObjectFactory
    {
        private readonly IAssetProvider assetProvider;
        private readonly IGameObjectPool gameObjectPool;

        public GameObjectFactory(IAssetProvider assetProvider, IGameObjectPool pool)
        {
            this.assetProvider = assetProvider;
            this.gameObjectPool = pool;
        }

        public async Task<GameObject> CreateAsync(SpawnPrefabInfo spawnInfo)
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

        private GameObject GetPoolable(GameObject prefab, SpawnPrefabInfo spawnInfo)
        {
            if (gameObjectPool.HasObjects(prefab))
            {
                return PullFromPool(prefab, spawnInfo);
            }
            else
            {
                return CreatePoolable(prefab, spawnInfo);
            }
        }

        private GameObject PullFromPool(GameObject prefab, SpawnPrefabInfo spawnInfo)
        {
            var go = gameObjectPool.Pull(prefab);
            SetGOParams(go, spawnInfo);
            return go;
        }

        private GameObject CreatePoolable(GameObject prefab, SpawnPrefabInfo spawnInfo)
        {
            var go = Create(prefab, spawnInfo);
            gameObjectPool.Register(prefab, go);
            return go;
        }

        private GameObject Create(GameObject prefab, SpawnPrefabInfo spawnInfo)
        {
            var go = Object.Instantiate(prefab, spawnInfo.Position, spawnInfo.Rotation, spawnInfo.Parent);
            SetGOParams(go, spawnInfo);
            return go;
        }

        private void SetGOParams(GameObject go, SpawnPrefabInfo spawnInfo)
        {
            go.transform.position = spawnInfo.Position;
            go.transform.rotation = spawnInfo.Rotation;
            go.transform.parent = spawnInfo.Parent;
        }
    }
}