using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Weapons.LaserGun
{
    public class LaserGunAttackSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var filter = world.Filter<CLaserGunTag>()
                              .Inc<CAttackRequest>()
                              .Inc<CAmmo>()
                              .Exc<CAttackCoolDown>()
                              .End();

            var ammoPool = world.GetPool<CAmmo>();

            foreach (var entity in filter)
            {
                ref var ammo = ref ammoPool.Get(entity).Value;

                if (ammo > 0)
                {
                    world.NewEntityWith(new CAttackEvent { PackedEntity = world.PackEntity(entity) });
                }
                else
                {
                    ammoPool.Del(entity);
                }
            }
        }
    }
}