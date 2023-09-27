using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.SecondaryWeapon
{
    public class SecondaryWeaponAttackSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<SecondaryWeaponAttackRequest>()
                              .Inc<SecondaryWeapon>()
                              .End();

            var weaponPool = world.GetPool<SecondaryWeapon>();
            var attackRequestPool = world.GetPool<AttackRequest>();

            foreach (var entity in filter)
            {
                ref var weaponPackedEntity = ref weaponPool.Get(entity).WeaponEntity;

                if (weaponPackedEntity.Unpack(world, out int weaponUnpackedEntity))
                {
                    attackRequestPool.Add(weaponUnpackedEntity);
                }
            }
        }
    }
}