using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.MaxVelocityMagnitude
{
    public class MaxVelocityMagnitudeConverter : IComponentConverter
    {
        public string TokenName => nameof(CMaxVelocityMagnitude);

        public object Convert(JToken token)
        {
            return new CMaxVelocityMagnitude { Value = (float)token };
        }
    }
}