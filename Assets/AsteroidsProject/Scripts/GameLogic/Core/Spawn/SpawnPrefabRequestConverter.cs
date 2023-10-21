using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class SpawnPrefabRequestConverter : IJTokenConverter
    {
        public string TokenName => nameof(CSpawnPrefabRequest);

        public object Convert(JToken token)
        {
            return new CSpawnPrefabRequest { PrefabAddress = (string)token };
        }
    }
}