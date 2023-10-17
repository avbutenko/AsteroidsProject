using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Input
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private readonly IInputService inputService;

        public PlayerInputSystem(IInputService inputService)
        {
            this.inputService = inputService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CPlayerTag>().End();

            var accelerationVectorPool = world.GetPool<CAccelerationVector>();
            var deaccelerationVectorPool = world.GetPool<CDeaccelerationVector>();
            var rotationDirectionPool = world.GetPool<CRotationDirection>();
            var primaryWeaponPool = world.GetPool<CPrimaryWeapon>();
            var secondaryWeaponPool = world.GetPool<CSecondaryWeapon>();
            var attackRequestPool = world.GetPool<AttackRequest>();

            foreach (var entity in filter)
            {
                if (inputService.IsAccelerating)
                {
                    if (!accelerationVectorPool.Has(entity))
                    {
                        accelerationVectorPool.Add(entity);
                    }

                    deaccelerationVectorPool.Del(entity);
                }

                if (inputService.IsDeaccelerating)
                {
                    deaccelerationVectorPool.Add(entity);
                    accelerationVectorPool.Del(entity);
                }

                if (inputService.IsRotating)
                {
                    rotationDirectionPool.Add(entity).Value = inputService.RotationDirection;
                }

                if (inputService.IsPrimaryWeaponAttackPerformed)
                {
                    ref var primaryWeaponPackedEntity = ref primaryWeaponPool.Get(entity).WeaponEntity;
                    AddWeaponAttackRequest(attackRequestPool, primaryWeaponPackedEntity, world);

                }

                if (inputService.IsSecondaryWeaponAttackPerformed)
                {
                    ref var secondaryWeaponPackedEntity = ref secondaryWeaponPool.Get(entity).WeaponEntity;
                    AddWeaponAttackRequest(attackRequestPool, secondaryWeaponPackedEntity, world);
                }
            }
        }

        private void AddWeaponAttackRequest(EcsPool<AttackRequest> attackRequestPool, EcsPackedEntity weaponPackedEntity, EcsWorld world)
        {
            if (weaponPackedEntity.Unpack(world, out int weaponUnpackedEntity))
            {
                attackRequestPool.Add(weaponUnpackedEntity);
            }
        }
    }
}