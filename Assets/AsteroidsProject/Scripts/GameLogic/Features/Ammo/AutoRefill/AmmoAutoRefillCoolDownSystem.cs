using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;

namespace AsteroidsProject.GameLogic.Features.Ammo.AutoRefill
{
    public class AmmoAutoRefillCoolDownSystem : CoolDownSystem<CAmmoAutoRefillCoolDown>
    {
        public AmmoAutoRefillCoolDownSystem(ITimeService timeService) : base(timeService) { }
    }
}