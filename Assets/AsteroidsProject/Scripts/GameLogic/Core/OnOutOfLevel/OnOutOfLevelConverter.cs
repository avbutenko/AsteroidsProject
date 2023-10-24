using AsteroidsProject.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace AsteroidsProject.GameLogic.Core
{
    public class OnOutOfLevelConverter : IJTokenConverter
    {
        private readonly IComponentConverterService componentConverterService;

        public OnOutOfLevelConverter(IComponentConverterService componentConverterService)
        {
            this.componentConverterService = componentConverterService;
        }

        public string TokenName => nameof(COnOutOfLevel);

        public object Convert(JToken token)
        {
            var serializerSettings = GetSerializerSettings();
            var list = JsonConvert.DeserializeObject<ComponentList>(token.ToString(), serializerSettings);
            return new COnOutOfLevel { Components = list.Components };
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