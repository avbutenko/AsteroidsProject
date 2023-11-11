using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;

namespace AsteroidsProject.GameLogic.Features.AttackCoolDown
{
    public class AttackCoolDownSystem : CoolDownSystem<CAttackCoolDown>
    {
        public AttackCoolDownSystem(ITimeService timeService) : base(timeService) { }
    }
}
