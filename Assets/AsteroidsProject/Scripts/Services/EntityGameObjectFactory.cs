using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class EntityGameObjectFactory : IEntityGameObjectFactory
    {
        private readonly IAssetProvider assetProvider;
        private readonly IGameObjectPool pool;

        public EntityGameObjectFactory(IAssetProvider assetProvider, IGameObjectPool pool)
        {
            this.assetProvider = assetProvider;
            this.pool = pool;
        }

        public async UniTask<GameObject> CreateAsync(SpawnEntityViewInfo spawnInfo)
        {
            var prefab = await assetProvider.LoadAsync<GameObject>(spawnInfo.PrefabAddress);

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

        private GameObject GetPoolable(GameObject prefab, SpawnEntityViewInfo spawnInfo)
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

        private GameObject PullFromPool(GameObject prefab, SpawnEntityViewInfo spawnInfo)
        {
            var go = pool.Pull(prefab);
            go.transform.SetPositionAndRotation(spawnInfo.Position, spawnInfo.Rotation);
            go.transform.parent = spawnInfo.Parent;
            return go;
        }

        private GameObject CreatePoolable(GameObject prefab, SpawnEntityViewInfo spawnInfo)
        {
            var go = Create(prefab, spawnInfo);
            pool.Register(prefab, go);
            return go;
        }

        private GameObject Create(GameObject prefab, SpawnEntityViewInfo spawnInfo)
        {
            return Object.Instantiate(prefab, spawnInfo.Position, spawnInfo.Rotation, spawnInfo.Parent);
        }
    }
}