using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Weapons.BulletGun
{
    public class BulletGunAttackSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var filter = world.Filter<CBulletGunTag>()
                              .Inc<CAttackRequest>()
                              .Inc<CCoolDown>()
                              .Exc<CActiveCoolDown>()
                              .End();

            var coolDownPool = world.GetPool<CCoolDown>();
            var activeCoolDownPool = world.GetPool<CActiveCoolDown>();
            var attackAllowedPool = world.GetPool<CAttackAllowed>();

            foreach (var entity in filter)
            {
                ref var coolDown = ref coolDownPool.Get(entity).Value;
                activeCoolDownPool.Add(entity).Value = coolDown;
                attackAllowedPool.Add(entity);
            }
        }
    }
}