using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Events
{
    public class DamageRequestConverter : IComponentConverter
    {
        public string TokenName => nameof(CDamageRequest);

        public object Convert(JToken token)
        {
            return new CDamageRequest { Value = (int)token };
        }
    }
}