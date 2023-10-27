using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Spawn.Weapons
{
    public class PrimaryWeaponConfigAddressConverter : IComponentConverter
    {
        public string TokenName => nameof(CPrimaryWeaponConfigAddress);

        public object Convert(JToken token)
        {
            return new CPrimaryWeaponConfigAddress { ConfigAddress = token.ToString() };
        }
    }
}