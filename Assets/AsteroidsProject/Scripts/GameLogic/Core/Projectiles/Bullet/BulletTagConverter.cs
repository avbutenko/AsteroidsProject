using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Projectiles.Bullet
{
    public class BulletTagConverter : IComponentConverter
    {
        public string TokenName => nameof(CBulletTag);

        public object Convert(JToken token)
        {
            return new CBulletTag();
        }
    }
}