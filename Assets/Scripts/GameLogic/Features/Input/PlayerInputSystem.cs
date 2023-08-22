using AsteroidsProject.GameLogic.Features.Rotation;
using AsteroidsProject.GameLogic.Features.Spawn;
using AsteroidsProject.Infrastructure.Services;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Movement
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

            var AccelerateCommandPool = world.GetPool<AccelerationCommand>();
            var RotateCommandPool = world.GetPool<RotateCommand>();

            foreach (var entityIndex in filter)
            {
                if (inputService.IsAccelerating)
                {
                    ref var accelerateCommand = ref AccelerateCommandPool.Add(entityIndex);
                }

                if (inputService.IsRotating)
                {
                    ref var rotateCommand = ref RotateCommandPool.Add(entityIndex);
                    rotateCommand.RotationDirection = inputService.RotationDirection;
                }
            }
        }
    }
}