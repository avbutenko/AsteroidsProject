using AsteroidsProject.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AsteroidsProject.Services
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> cachedObjects = new();

        public async Task<T> Load<T>(string address) where T : Object
        {
            return cachedObjects.TryGetValue(address, out var cachedHandle)
                ? cachedHandle.Result as T
                : await ResourceLoading<T>(address);
        }

        private async Task<T> ResourceLoading<T>(string address)
        {
            var handle = Addressables.LoadAssetAsync<T>(address);
            handle.Completed += operationHandle => cachedObjects[address] = operationHandle;
            return await handle.Task;
        }
    }
}