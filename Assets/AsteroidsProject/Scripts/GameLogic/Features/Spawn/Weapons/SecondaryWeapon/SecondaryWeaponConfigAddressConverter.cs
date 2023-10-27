using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Spawn.Weapons
{
    public class SecondaryWeaponConfigAddressConverter : IComponentConverter
    {
        public string TokenName => nameof(CSecondaryWeaponConfigAddress);

        public object Convert(JToken token)
        {
            return new CSecondaryWeaponConfigAddress { ConfigAddress = token.ToString() };
        }
    }
}