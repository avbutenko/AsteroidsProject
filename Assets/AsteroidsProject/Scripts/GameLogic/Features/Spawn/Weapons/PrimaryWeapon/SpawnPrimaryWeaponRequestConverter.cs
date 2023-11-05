using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Spawn.Weapons
{
    public class SpawnPrimaryWeaponRequestConverter : IComponentConverter
    {
        public string TokenName => nameof(CSpawnPrimaryWeaponRequest);

        public object Convert(JToken token)
        {
            return new CSpawnPrimaryWeaponRequest { Config = (string)token };
        }
    }
}