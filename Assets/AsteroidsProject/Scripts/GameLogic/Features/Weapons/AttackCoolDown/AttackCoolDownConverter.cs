using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Weapons
{
    public class AttackCoolDownConverter : IComponentConverter
    {
        public string TokenName => nameof(CAttackCoolDown);

        public object Convert(JToken token)
        {
            return new CAttackCoolDown { Value = (float)token };
        }
    }
}