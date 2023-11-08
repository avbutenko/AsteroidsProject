using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class AsteroidFragmentTagConverter : IComponentConverter
    {
        public string TokenName => nameof(CAsteroidFragmentTag);

        public object Convert(JToken token)
        {
            return new CAsteroidFragmentTag();
        }
    }
}
