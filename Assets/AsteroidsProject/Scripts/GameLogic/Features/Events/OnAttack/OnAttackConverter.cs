using AsteroidsProject.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Events.OnAttack
{
    public class OnAttackConverter : ComponentListConverter<COnAttack>
    {
        public OnAttackConverter(IComponentConverterService componentConverterService) : base(componentConverterService) { }

        public override object Convert(JToken token)
        {
            var serializerSettings = GetSerializerSettings();
            var data = JsonConvert.DeserializeObject<COnAttack>(token.ToString(), serializerSettings);
            return new COnAttack { AddToSelfComponents = data.AddToSelfComponents };
        }
    }
}