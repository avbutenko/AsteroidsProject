using AsteroidsProject.Infrastructure.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AsteroidsProject.EngineRelated.Services
{
    public class AssetProvider : IAssetProvider
    {

        private readonly Dictionary<string, AsyncOperationHandle> cache = new();

        public async Task<TAsset> Load<TAsset>(string address) where TAsset : Object
        {
            return cache.TryGetValue(address, out var cachedHandle)
                ? cachedHandle.Result as TAsset
                : await ResourceLoading<TAsset>(address);
        }

        private async Task<TResource> ResourceLoading<TResource>(string address)
        {
            var handle = Addressables.LoadAssetAsync<TResource>(address);
            handle.Completed += operationHandle => cache[address] = operationHandle;
            return await handle.Task;
        }
    }
}