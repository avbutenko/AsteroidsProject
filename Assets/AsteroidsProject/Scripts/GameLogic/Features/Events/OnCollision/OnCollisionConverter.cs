using AsteroidsProject.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AsteroidsProject.GameLogic.Features.Events
{
    public class OnCollisionConverter : IComponentConverter
    {
        private readonly IComponentConverterService componentConverterService;

        public OnCollisionConverter(IComponentConverterService componentConverterService)
        {
            this.componentConverterService = componentConverterService;
        }

        public string TokenName => nameof(COnCollision);

        public object Convert(JToken token)
        {
            var serializerSettings = GetSerializerSettings();
            var data = JsonConvert.DeserializeObject<List<OnCollisionParams>>(token.ToString(), serializerSettings);
            return new COnCollision { Params = data };
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
