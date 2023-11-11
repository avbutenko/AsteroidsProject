using AsteroidsProject.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Ammo.AutoRefill
{
    public class AmmoAutoRefillConverter : IComponentConverter
    {
        public string TokenName => nameof(CAmmoAutoRefill);

        public object Convert(JToken token)
        {
            var data = JsonConvert.DeserializeObject<CAmmoAutoRefill>(token.ToString());
            return new CAmmoAutoRefill { Timer = data.Timer, Amount = data.Amount };
        }
    }
}