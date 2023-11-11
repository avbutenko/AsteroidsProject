using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Ammo.Max
{
    public class AmmoMaxConverter : IComponentConverter
    {
        public string TokenName => nameof(CAmmoMax);

        public object Convert(JToken token)
        {
            return new CAmmoMax { Value = (int)token };
        }
    }
}