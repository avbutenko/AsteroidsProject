using AsteroidsProject.Shared;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System;

namespace AsteroidsProject.Services
{
    public class ConfigProvider : IConfigProvider, IInitializable, IDisposable
    {
        private const string gameConfigPath = "GameConfig";
        private readonly IComponentConverterService componentConverterService;
        private readonly IAssetProvider assetProvider;
        private readonly Dictionary<string, object> cachedObjects;
        private JsonSerializerSettings serializerSettings;

        public ConfigProvider(IAssetProvider assetProvider, IComponentConverterService componentConverterService)
        {
            this.assetProvider = assetProvider;
            this.componentConverterService = componentConverterService;
            cachedObjects = new Dictionary<string, object>();
        }

        public string GameConfigPath => gameConfigPath;

        public void Initialize()
        {
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