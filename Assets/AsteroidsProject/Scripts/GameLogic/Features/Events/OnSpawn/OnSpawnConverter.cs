using AsteroidsProject.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Events
{
    public class OnSpawnConverter : ComponentListConverter<COnSpawn>
    {
        public OnSpawnConverter(IComponentConverterService componentConverterService) : base(componentConverterService) { }

        public override object Convert(JToken token)
        {
            var serializerSettings = GetSerializerSettings();
            var data = JsonConvert.DeserializeObject<COnSpawn>(token.ToString(), serializerSettings);
            return new COnSpawn { AddToSelfComponents = data.AddToSelfComponents };
        }
    }
}