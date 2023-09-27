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
            var primaryWeaponAttackRequestPool = world.GetPool<PrimaryWeaponAttackRequest>();
            var secondaryWeaponAttackRequestPool = world.GetPool<SecondaryWeaponAttackRequest>();

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
                    primaryWeaponAttackRequestPool.Add(entity);
                }

                if (inputService.IsSecondaryWeaponAttackPerformed)
                {
                    secondaryWeaponAttackRequestPool.Add(entity);
                }
            }
        }
    }
}