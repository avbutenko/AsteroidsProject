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
            var filter = world.Filter<PlayerTag>().End();

            var accelerationRequestPool = world.GetPool<AccelerationRequest>();
            var deaccelerationRequestPool = world.GetPool<DeaccelerationRequest>();
            var rotationDirectionPool = world.GetPool<RotationDirection>();
            var primaryWeaponPool = world.GetPool<PrimaryWeapon>();
            var secondaryWeaponPool = world.GetPool<SecondaryWeapon>();
            var attackRequestPool = world.GetPool<AttackRequest>();

            foreach (var entity in filter)
            {
                if (inputService.IsAccelerating)
                {
                    accelerationRequestPool.Add(entity);
                    deaccelerationRequestPool.Del(entity);
                }

                if (inputService.IsDeaccelerating)
                {
                    deaccelerationRequestPool.Add(entity);
                    accelerationRequestPool.Del(entity);
                }

                ref var rotationDirection = ref rotationDirectionPool.Get(entity).Value;
                rotationDirection = inputService.RotationDirection;

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