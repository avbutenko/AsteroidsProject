using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;

namespace AsteroidsProject.GameLogic.Features.Ammo.AutoRefill
{
    public class AmmoAutoRefillCoolDownSystem : BaseCoolDownSystem<CAmmoAutoRefillCoolDown>
    {
        public AmmoAutoRefillCoolDownSystem(ITimeService timeService) : base(timeService) { }
    }
}