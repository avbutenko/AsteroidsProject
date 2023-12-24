using AsteroidsProject.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Events
{
    public class OnOutOfLevelConverter : ComponentListConverter<COnOutOfLevel>
    {
        public OnOutOfLevelConverter(IComponentConverterService componentConverterService) : base(componentConverterService) { }

        public override object Convert(JToken token)
        {
            var serializerSettings = GetSerializerSettings();
            var data = JsonConvert.DeserializeObject<COnOutOfLevel>(token.ToString(), serializerSettings);
            return new COnOutOfLevel { AddToSelfComponents = data.AddToSelfComponents };
        }
    }
}