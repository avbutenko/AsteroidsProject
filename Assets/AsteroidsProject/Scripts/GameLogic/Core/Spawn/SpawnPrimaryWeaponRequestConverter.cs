using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class SpawnPrimaryWeaponRequestConverter : IJTokenConverter
    {
        public string TokenName => nameof(CSpawnPrimaryWeaponRequest);

        public object Convert(JToken token)
        {
            return new CSpawnPrimaryWeaponRequest { ConfigAddress = token.ToString() };
        }
    }
}