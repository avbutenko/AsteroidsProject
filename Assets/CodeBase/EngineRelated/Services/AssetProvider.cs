using AsteroidsProject.CodeBase.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AsteroidsProject.CodeBase.Services
{
    public class AssetProvider : IAssetProvider
    {

        private readonly Dictionary<string, AsyncOperationHandle> cache = new();

        public async Task<TAsset> LoadAsync<TAsset>(string address) where TAsset : Object
        {
            return cache.TryGetValue(address, out var cachedHandle)
                ? cachedHandle.Result as TAsset
                : await ResourceLoading<TAsset>(address);
        }

        //public void UnLoad()
        //{
        //    foreach (var pair in cache)
        //        Addressables.Release(pair.Value);
        //    cache.Clear();
        //}

        private async Task<TResource> ResourceLoading<TResource>(string address)
        {
            var handle = Addressables.LoadAssetAsync<TResource>(address);
            handle.Completed += operationHandle => cache[address] = operationHandle;
            return await handle.Task;
        }
    }
}