using AsteroidsProject.Shared;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace AsteroidsProject.Services
{
    public class ConfigLoader : IConfigLoader, IInitializable
    {
        private readonly IComponentConverterService componentConverterService;
        private readonly IAssetProvider assetProvider;
        private Dictionary<string, object> cachedObjects;
        private JsonSerializerSettings serializerSettings;

        public ConfigLoader(IAssetProvider assetProvider, IComponentConverterService componentConverterService)
        {
            this.assetProvider = assetProvider;
            this.componentConverterService = componentConverterService;
        }

        public void Initialize()
        {
            cachedObjects = new Dictionary<string, object>();

            serializerSettings = new JsonSerializerSettings
            {
                Context = new StreamingContext(StreamingContextStates.All, componentConverterService)
            };
        }

        public async UniTask<T> Load<T>(string configAddress) where T : class
        {
            return cachedObjects.TryGetValue(configAddress, out var cachedObject)
                ? cachedObject as T
                : await ResourceLoading<T>(configAddress);
        }

        public void Dispose()
        {
            cachedObjects.Clear();
        }

        private async UniTask<T> ResourceLoading<T>(string configAddress) where T : class
        {
            var asset = await assetProvider.LoadAsync<TextAsset>(configAddress);
            var result = JsonConvert.DeserializeObject<T>(asset.text, serializerSettings);
            cachedObjects[configAddress] = result;
            return result;
        }
    }
}