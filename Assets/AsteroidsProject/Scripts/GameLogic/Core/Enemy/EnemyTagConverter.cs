using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class EnemyTagConverter : IJTokenConverter
    {
        public string TokenName => nameof(CEnemyTag);

        public object Convert(JToken token)
        {
            return new CEnemyTag();
        }
    }
}