using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Movement.Deacceleration
{
    public class DeaccelerationModifierConverter : IComponentConverter
    {
        public string TokenName => nameof(CDeaccelerationModifier);

        public object Convert(JToken token)
        {
            return new CDeaccelerationModifier { Value = (float)token };
        }
    }
}