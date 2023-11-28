using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AsteroidsProject.Services
{
    public class AssetProvider : IAssetProvider, IDisposable
    {
        private readonly Dictionary<string, AsyncOperationHandle> cachedObjects = new();

        public async UniTask<T> LoadAsync<T>(string address) where T : UnityEngine.Object
        {
            return cachedObjects.TryGetValue(address, out var cachedHandle)
                ? cachedHandle.Result as T
                : await ResourceLoading<T>(address);
        }

        private async UniTask<T> ResourceLoading<T>(string address)
        {
            var handle = Addressables.LoadAssetAsync<T>(address);
            handle.Completed += operationHandle => cachedObjects[address] = operationHandle;
            return await handle.Task;
        }

        public void Dispose()
        {
            foreach (var item in cachedObjects)
            {
                Addressables.Release(item.Value);
            }

            cachedObjects.Clear();
        }
    }
}