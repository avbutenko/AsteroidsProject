using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Weapons.BulletGun
{
    public class BulletGunTagConverter : IComponentConverter
    {
        public string TokenName => nameof(CBulletGunTag);

        public object Convert(JToken token)
        {
            return new CBulletGunTag { };
        }
    }
}