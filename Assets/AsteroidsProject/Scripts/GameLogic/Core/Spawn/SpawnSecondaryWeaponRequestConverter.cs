using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class SpawnSecondaryWeaponRequestConverter : IJTokenConverter
    {
        public string TokenName => nameof(CSpawnSecondaryWeaponRequest);

        public object Convert(JToken token)
        {
            return new CSpawnSecondaryWeaponRequest { WeaponConfig = token.ToString() };
        }
    }
}