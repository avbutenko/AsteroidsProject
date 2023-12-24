using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class LaserTagConverter : IComponentConverter
    {
        public string TokenName => nameof(CLaserTag);

        public object Convert(JToken token)
        {
            return new CLaserTag();
        }
    }
}