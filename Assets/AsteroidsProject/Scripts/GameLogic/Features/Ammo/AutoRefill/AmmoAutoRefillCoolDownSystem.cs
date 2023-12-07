using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Ammo.AutoRefill
{
    public class AmmoAutoRefillCoolDownSystem : BaseCoolDownSystem<CAmmoAutoRefillCoolDown>
    {
        private EcsPool<CAmmoAutoRefillRequest> requestPool;
        public AmmoAutoRefillCoolDownSystem(ITimeService timeService) : base(timeService) { }

        public override void Init(IEcsSystems systems)
        {
            base.Init(systems);
            requestPool = world.GetPool<CAmmoAutoRefillRequest>();
        }
        protected override void OnTimeIsUp(int entity)
        {
            base.OnTimeIsUp(entity);
            requestPool.Add(entity);
        }
    }
}