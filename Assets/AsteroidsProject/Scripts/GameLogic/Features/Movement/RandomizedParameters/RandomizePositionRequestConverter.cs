using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class RandomizePositionRequestConverter : IComponentConverter
    {
        public string TokenName => nameof(CRandomizePositionRequest);

        public object Convert(JToken token)
        {
            return new CRandomizePositionRequest();
        }
    }
}