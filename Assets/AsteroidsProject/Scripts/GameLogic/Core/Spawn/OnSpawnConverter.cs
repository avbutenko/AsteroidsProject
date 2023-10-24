using AsteroidsProject.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace AsteroidsProject.GameLogic.Core
{
    public class OnSpawnConverter : IJTokenConverter
    {
        private readonly IComponentConverterService componentConverterService;

        public OnSpawnConverter(IComponentConverterService componentConverterService)
        {
            this.componentConverterService = componentConverterService;
        }

        public string TokenName => nameof(COnSpawn);

        public object Convert(JToken token)
        {
            var serializerSettings = GetSerializerSettings();
            var list = JsonConvert.DeserializeObject<ComponentList>(token.ToString(), serializerSettings);
            return new COnSpawn { Components = list.Components };
        }

        private JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                Context = new StreamingContext(StreamingContextStates.All, componentConverterService)
            };
        }
    }
}