using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class LaserTagConverter : IJTokenConverter
    {
        public string TokenName => nameof(CLaserTag);

        public object Convert(JToken token)
        {
            return new CLaserTag();
        }
    }
}