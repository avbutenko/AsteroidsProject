using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Spawn.Projectiles
{
    public class ProjectileConfigConverter : IComponentConverter
    {
        public string TokenName => nameof(CProjectileConfig);

        public object Convert(JToken token)
        {
            return new CProjectileConfig { Config = token.ToString() };
        }
    }
}