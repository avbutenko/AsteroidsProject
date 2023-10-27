using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
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