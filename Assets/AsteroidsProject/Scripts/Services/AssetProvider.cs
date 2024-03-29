using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AsteroidsProject.Services
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> cachedObjects = new();

        public async UniTask<T> LoadAsync<T>(string address) where T : class
        {

            if (!cachedObjects.TryGetValue(address, out AsyncOperationHandle handle))
            {
                handle = Addressables.LoadAssetAsync<T>(address);
                cachedObjects.Add(address, handle);
            }

            await handle.ToUniTask();

            return handle.Result as T;
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

        public async UniTask PreLoadAllByLabelAsync(string label)
        {
            var assetsList = await GetAddressListByLabel(label);
            await LoadAsync<object>(assetsList);
        }

        public async UniTask<T[]> LoadAllByLabelAsync<T>(string label) where T : class
        {
            var assetsList = await GetAddressListByLabel(label);
            return await LoadAsync<T>(assetsList);
        }

        public async UniTask<T> ResourceLoadAsync<T>(string resourceAddress) where T : class
        {
            return await Resources.LoadAsync(resourceAddress) as T;
        }

        public T ResourceLoad<T>(string resourceAddress) where T : class
        {
            return Resources.Load(resourceAddress) as T;
        }

        public void Dispose()
        {
            foreach (var item in cachedObjects)
            {
                Addressables.Release(item.Value);
            }

            cachedObjects.Clear();
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