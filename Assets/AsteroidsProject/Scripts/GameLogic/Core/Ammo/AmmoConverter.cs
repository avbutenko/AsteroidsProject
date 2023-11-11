using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class AmmoConverter : IComponentConverter
    {
        public string TokenName => nameof(CAmmo);

        public object Convert(JToken token)
        {
            return new CAmmo { Value = (int)token };
        }
    }
}