using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Weapons.BulletGun
{
    public class BulletGunAttackSystem : IEcsRunSystem
    {
        private readonly IActiveGOMappingService activeGOMappingService;

        public BulletGunAttackSystem(IActiveGOMappingService activeGOMappingService)
        {
            this.activeGOMappingService = activeGOMappingService;
        }

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
            var goIDPool = world.GetPool<CGameObjectInstanceID>();

            foreach (var entity in filter)
            {
                if (!goIDPool.Has(entity)) return;

                ref var goID = ref goIDPool.Get(entity).Value;
                if (!activeGOMappingService.TryGetGoLink(goID, out var goLink)) return;

                var shootingPoint = goLink as IHaveShootingPoint;
                ref var coolDown = ref coolDownPool.Get(entity).Value;
                spawnProjectileRequestPool.Add(entity).ShootingPoint = shootingPoint;
                activeCoolDownPool.Add(entity).Value = coolDown;
            }
        }
    }
}