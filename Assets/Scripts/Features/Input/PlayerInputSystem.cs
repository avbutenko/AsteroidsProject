using AsteroidsProject.Features.Core;
using AsteroidsProject.Infrastructure.Services;
using Leopotam.EcsLite;

namespace AsteroidsProject.Features.Input
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

            var accelerateCommandPool = world.GetPool<ForwardAccelerationRequest>();
            var inertCommandPool = world.GetPool<DeaccelerationRequest>();
            var rotateCommandPool = world.GetPool<RotationRequest>();

            foreach (var entity in filter)
            {
                if (inputService.IsAccelerating)
                {
                    accelerateCommandPool.Add(entity);
                }
                else
                {
                    inertCommandPool.Add(entity);
                }

                if (inputService.IsRotating)
                {
                    ref var rotateCommand = ref rotateCommandPool.Add(entity);
                    rotateCommand.RotationDirection = inputService.RotationDirection;
                }
            }
        }
    }
}