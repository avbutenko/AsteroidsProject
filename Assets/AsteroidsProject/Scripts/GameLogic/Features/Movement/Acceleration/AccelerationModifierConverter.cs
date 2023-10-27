using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Movement.Acceleration
{
    public class AccelerationModifierConverter : IComponentConverter
    {
        public string TokenName => nameof(CAccelerationModifier);

        public object Convert(JToken token)
        {
            return new CAccelerationModifier { Value = (float)token };
        }
    }
}