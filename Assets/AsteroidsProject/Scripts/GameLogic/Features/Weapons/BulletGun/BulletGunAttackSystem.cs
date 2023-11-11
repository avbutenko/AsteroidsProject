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
                              .Exc<CAttackCoolDown>()
                              .End();

            foreach (var entity in filter)
            {
                world.NewEntityWith(new CAttackEvent { PackedEntity = world.PackEntity(entity) });
            }
        }
    }
}