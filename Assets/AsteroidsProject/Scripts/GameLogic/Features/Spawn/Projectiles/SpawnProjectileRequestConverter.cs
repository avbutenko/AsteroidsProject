using AsteroidsProject.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Spawn
{
    public class SpawnProjectileRequestConverter : IComponentConverter
    {
        public string TokenName => nameof(CSpawnProjectileRequest);

        public object Convert(JToken token)
        {
            var data = JsonConvert.DeserializeObject<CSpawnProjectileRequest>(token.ToString());
            return new CSpawnProjectileRequest { Config = data.Config, ParentType = data.ParentType };
        }
    }
}