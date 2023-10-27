using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class CoolDownConverter : IComponentConverter
    {
        public string TokenName => nameof(CCoolDown);

        public object Convert(JToken token)
        {
            return new CCoolDown { Value = (float)token };
        }
    }
}