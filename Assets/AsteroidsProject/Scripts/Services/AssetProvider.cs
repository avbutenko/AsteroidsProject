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

        public async UniTask<T> LoadAsync<T>(string address) where T : class
        {
            return cachedObjects.TryGetValue(address, out var cachedHandle)
                ? cachedHandle.Result as T
                : await ResourceLoading<T>(address);
        }

        public async UniTask<T[]> LoadAsync<T>(List<string> addresses) where T : class
        {
            var tasks = new List<UniTask<T>>(addresses.Count);

            foreach (var key in addresses)
            {
                tasks.Add(LoadAsync<T>(key));
            }

            return await UniTask.WhenAll(tasks);
        }

        public async UniTask PreLoadAsyncByLabel(string label)
        {
            var assetsList = await GetAddressListByLabel(label);
            await LoadAsync<object>(assetsList);
        }

        public void Dispose()
        {
            foreach (var item in cachedObjects)
            {
                Addressables.Release(item.Value);
            }

            cachedObjects.Clear();
        }

        private async UniTask<T> ResourceLoading<T>(string address)
        {
            var handle = Addressables.LoadAssetAsync<T>(address);
            handle.Completed += operationHandle => cachedObjects[address] = operationHandle;
            return await handle.Task.AsUniTask();
        }

        private async UniTask<List<string>> GetAddressListByLabel(string label, Type type = null)
        {
            var handle = Addressables.LoadResourceLocationsAsync(label, type);
            var locationList = await handle.ToUniTask();
            var addressList = new List<string>(locationList.Count);

            foreach (var location in locationList)
            {
                addressList.Add(location.PrimaryKey);
            }

            Addressables.Release(handle);
            return addressList;
        }
    }
}