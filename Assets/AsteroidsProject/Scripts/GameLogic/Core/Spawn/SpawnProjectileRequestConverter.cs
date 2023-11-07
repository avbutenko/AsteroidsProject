using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class SpawnProjectileRequestConverter : IComponentConverter
    {
        public string TokenName => nameof(CSpawnProjectileRequest);

        public object Convert(JToken token)
        {
            return new CSpawnProjectileRequest { Config = token.ToString() };
        }
    }
}