using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;

namespace AsteroidsProject.GameLogic.Features.Weapons
{
    public class AttackCoolDownSystem : BaseCoolDownSystem<CAttackCoolDown>
    {
        public AttackCoolDownSystem(ITimeService timeService) : base(timeService) { }
    }
}
