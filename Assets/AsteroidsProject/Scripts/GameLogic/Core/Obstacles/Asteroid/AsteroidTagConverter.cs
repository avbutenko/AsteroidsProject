using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class AsteroidTagConverter : IComponentConverter
    {
        public string TokenName => nameof(CAsteroidTag);

        public object Convert(JToken token)
        {
            return new CAsteroidTag();
        }
    }
}