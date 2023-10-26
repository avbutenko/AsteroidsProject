using AsteroidsProject.Shared;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace AsteroidsProject.Services
{
    public class ConfigProvider : IConfigProvider, IInitializable
    {
        private const string gameConfigPath = "Configs/GameConfig.json";
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

        public async Task<T> Load<T>(string configAddress) where T : class
        {
            return cachedObjects.TryGetValue(configAddress, out var cachedObject)
                ? cachedObject as T
                : await ResourceLoading<T>(configAddress);
        }

        private async Task<T> ResourceLoading<T>(string configAddress) where T : class
        {
            var asset = await assetProvider.Load<TextAsset>(configAddress);
            var result = JsonConvert.DeserializeObject<T>(asset.text, serializerSettings);
            cachedObjects[configAddress] = result;
            return result;
        }
    }
}