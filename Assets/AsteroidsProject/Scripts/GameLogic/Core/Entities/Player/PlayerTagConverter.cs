using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class PlayerTagConverter : IComponentConverter
    {
        public string TokenName => nameof(CPlayerTag);

        public object Convert(JToken token)
        {
            return new CPlayerTag();
        }
    }
}