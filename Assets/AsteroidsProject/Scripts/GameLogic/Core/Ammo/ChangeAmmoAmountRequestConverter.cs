using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class ChangeAmmoAmountRequestConverter : IComponentConverter
    {
        public string TokenName => nameof(CChangeAmmoAmountRequest);

        public object Convert(JToken token)
        {
            return new CChangeAmmoAmountRequest { Value = (int)token };
        }
    }
}