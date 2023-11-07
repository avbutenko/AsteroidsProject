using AsteroidsProject.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AsteroidsProject.GameLogic.Features.Events.OnDeath
{
    public class OnDeathConverter : IComponentConverter
    {
        private readonly IComponentConverterService componentConverterService;

        public OnDeathConverter(IComponentConverterService componentConverterService)
        {
            this.componentConverterService = componentConverterService;
        }

        public string TokenName => nameof(COnDeath);

        public object Convert(JToken token)
        {
            var serializerSettings = GetSerializerSettings();
            var data = JsonConvert.DeserializeObject<List<ComponentList>>(token.ToString(), serializerSettings);
            return new COnDeath { CreateEntities = data };
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