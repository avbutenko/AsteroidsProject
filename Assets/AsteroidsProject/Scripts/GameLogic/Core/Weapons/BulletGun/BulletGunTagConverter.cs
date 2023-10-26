using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class BulletGunTagConverter : IJTokenConverter
    {
        public string TokenName => nameof(CBulletGunTag);

        public object Convert(JToken token)
        {
            return new CBulletGunTag { };
        }
    }
}