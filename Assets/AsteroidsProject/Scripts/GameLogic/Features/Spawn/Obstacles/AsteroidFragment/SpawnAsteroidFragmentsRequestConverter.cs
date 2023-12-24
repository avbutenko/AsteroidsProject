using AsteroidsProject.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Spawn
{
    public class SpawnAsteroidFragmentsRequestConverter : IComponentConverter
    {
        public string TokenName => nameof(CSpawnAsteroidFragmentsRequest);

        public object Convert(JToken token)
        {
            var data = JsonConvert.DeserializeObject<CSpawnAsteroidFragmentsRequest>(token.ToString());
            return new CSpawnAsteroidFragmentsRequest { Config = data.Config, Amount = data.Amount };
        }
    }
}