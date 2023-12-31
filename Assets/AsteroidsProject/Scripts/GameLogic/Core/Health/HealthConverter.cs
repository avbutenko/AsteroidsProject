using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class HealthConverter : IComponentConverter
    {
        public string TokenName => nameof(CHealth);

        public object Convert(JToken token)
        {
            return new CHealth { Value = (int)token };
        }
    }
}