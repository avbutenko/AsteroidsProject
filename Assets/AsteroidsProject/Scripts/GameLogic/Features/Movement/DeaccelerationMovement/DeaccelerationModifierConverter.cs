using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.DeaccelerationMovement
{
    public class DeaccelerationModifierConverter : IJTokenConverter
    {
        public string TokenName => nameof(CDeaccelerationModifier);

        public object Convert(JToken token)
        {
            return new CDeaccelerationModifier { Value = (float)token };
        }
    }
}