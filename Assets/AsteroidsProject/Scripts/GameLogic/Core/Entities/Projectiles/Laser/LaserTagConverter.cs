using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Projectiles.Laser
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