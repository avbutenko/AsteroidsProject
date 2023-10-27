using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
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

            var spawnProjectileRequestPool = world.GetPool<CSpawnProjectileRequest>();
            var coolDownPool = world.GetPool<CCoolDown>();
            var activeCoolDownPool = world.GetPool<CActiveCoolDown>();
            var goLink = world.GetPool<CGameObject>();

            foreach (var entity in filter)
            {
                if (!goLink.Has(entity)) return;

                ref var view = ref goLink.Get(entity).Link;
                var shootingPoint = view as IHaveShootingPoint;
                ref var coolDown = ref coolDownPool.Get(entity).Value;
                spawnProjectileRequestPool.Add(entity).ShootingPoint = shootingPoint;
                activeCoolDownPool.Add(entity).Value = coolDown;
            }
        }
    }
}