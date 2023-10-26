using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class LaserGunTagConverter : IJTokenConverter
    {
        public string TokenName => nameof(CLaserGunTag);

        public object Convert(JToken token)
        {
            return new CLaserGunTag { };
        }
    }
}