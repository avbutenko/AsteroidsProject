using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Spawn.Weapons
{
    public class SpawnSecondaryWeaponRequestConverter : IComponentConverter
    {
        public string TokenName => nameof(CSpawnSecondaryWeaponRequest);

        public object Convert(JToken token)
        {
            return new CSpawnSecondaryWeaponRequest { };
        }
    }
}