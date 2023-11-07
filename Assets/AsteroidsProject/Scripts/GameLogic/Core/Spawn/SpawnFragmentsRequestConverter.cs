using AsteroidsProject.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class SpawnFragmentsRequestConverter : IComponentConverter
    {
        public string TokenName => nameof(CSpawnFragmentsRequest);

        public object Convert(JToken token)
        {
            var data = JsonConvert.DeserializeObject<CSpawnFragmentsRequest>(token.ToString());
            return new CSpawnFragmentsRequest { Config = data.Config, Amount = data.Amount };
        }
    }
}