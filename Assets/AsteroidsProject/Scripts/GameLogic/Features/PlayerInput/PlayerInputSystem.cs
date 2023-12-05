using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.PlayerInput
{
    public class PlayerInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IInputService inputService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CAccelerationVector> accelerationVectorPool;
        private EcsPool<CDeaccelerationVector> deaccelerationVectorPool;
        private EcsPool<CRotationDirection> rotationDirectionPool;
        private EcsPool<CPrimaryWeapon> primaryWeaponPool;
        private EcsPool<CSecondaryWeapon> secondaryWeaponPool;
        private EcsPool<CAttackRequest> attackRequestPool;

        public PlayerInputSystem(IInputService inputService)
        {
            this.inputService = inputService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CPlayerTag>().End();
            accelerationVectorPool = world.GetPool<CAccelerationVector>();
            deaccelerationVectorPool = world.GetPool<CDeaccelerationVector>();
            rotationDirectionPool = world.GetPool<CRotationDirection>();
            primaryWeaponPool = world.GetPool<CPrimaryWeapon>();
            secondaryWeaponPool = world.GetPool<CSecondaryWeapon>();
            attackRequestPool = world.GetPool<CAttackRequest>();
        }

        public void Run(IEcsSystems systems)
        {
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

                if (inputService.IsPrimaryWeaponAttackPerformed && primaryWeaponPool.Has(entity))
                {
                    ref var primaryWeaponPackedEntity = ref primaryWeaponPool.Get(entity).WeaponEntity;
                    AddWeaponAttackRequest(primaryWeaponPackedEntity);
                }

                if (inputService.IsSecondaryWeaponAttackPerformed && secondaryWeaponPool.Has(entity))
                {
                    ref var secondaryWeaponPackedEntity = ref secondaryWeaponPool.Get(entity).WeaponEntity;
                    AddWeaponAttackRequest(secondaryWeaponPackedEntity);
                }

                if (inputService.IsPausePerformed)
                {
                    world.NewEntityWith<CGamePauseEvent>();
                }
            }
        }

        private void AddWeaponAttackRequest(EcsPackedEntity weaponPackedEntity)
        {
            if (weaponPackedEntity.Unpack(world, out int weaponUnpackedEntity))
            {
                attackRequestPool.Add(weaponUnpackedEntity);
            }
        }
    }
}