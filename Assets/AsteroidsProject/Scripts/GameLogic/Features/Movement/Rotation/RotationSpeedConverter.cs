using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class RotationSpeedConverter : IComponentConverter
    {
        public string TokenName => nameof(CRotationSpeed);

        public object Convert(JToken token)
        {
            return new CRotationSpeed { Value = (float)token };
        }
    }
}