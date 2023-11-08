using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Lifetime
{
    public class LifetimeConverter : IComponentConverter
    {
        public string TokenName => nameof(CLifetime);

        public object Convert(JToken token)
        {
            return new CLifetime { Value = (int)token };
        }
    }
}