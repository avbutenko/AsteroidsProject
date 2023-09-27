using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.PrimaryWeapon
{
    public class PrimaryWeaponAttackSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<PrimaryWeaponAttackRequest>()
                              .Inc<PrimaryWeapon>()
                              .End();

            var primaryWeaponPool = world.GetPool<PrimaryWeapon>();
            var attackRequestPool = world.GetPool<AttackRequest>();

            foreach (var entity in filter)
            {
                ref var primaryWeaponPackedEntity = ref primaryWeaponPool.Get(entity).WeaponEntity;

                if (primaryWeaponPackedEntity.Unpack(world, out int primaryWeaponUnpackedEntity))
                {
                    attackRequestPool.Add(primaryWeaponUnpackedEntity);
                }
            }
        }
    }
}