using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Weapons.LaserGun
{
    public class LaserGunTagConverter : IComponentConverter
    {
        public string TokenName => nameof(CLaserGunTag);

        public object Convert(JToken token)
        {
            return new CLaserGunTag { };
        }
    }
}