using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.AccelerationMovement
{
    public class AccelerationModifierConverter : IJTokenConverter
    {
        public string TokenName => nameof(CAccelerationModifier);

        public object Convert(JToken token)
        {
            return new CAccelerationModifier { Value = (float)token };
        }
    }
}