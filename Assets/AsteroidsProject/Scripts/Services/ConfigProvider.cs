using AsteroidsProject.Shared;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;
using System.Runtime.Serialization;

namespace AsteroidsProject.Services
{
    public class ConfigProvider : IConfigProvider, IInitializable
    {
        private IComponentConverterService componentConverterService;
        private readonly IAssetProvider assetProvider;
        private JsonSerializerSettings serializerSettings;
        public ConfigProvider(IAssetProvider assetProvider, IComponentConverterService componentConverterService)
        {
            this.assetProvider = assetProvider;
            this.componentConverterService = componentConverterService;
        }

        public void Initialize()
        {
            serializerSettings = new JsonSerializerSettings
            {
                Context = new StreamingContext(StreamingContextStates.All, componentConverterService)
            };
        }

        public async Task<T> Load<T>(string configAddress) where T : class
        {
            var asset = await assetProvider.Load<TextAsset>(configAddress);
            return JsonConvert.DeserializeObject<T>(asset.text, serializerSettings);
        }
    }
}