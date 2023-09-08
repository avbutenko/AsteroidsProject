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

            var accelerationRequestPool = world.GetPool<ForwardAccelerationRequest>();
            var deaccelerationRequestPool = world.GetPool<DeaccelerationRequest>();
            var rotationRequestPool = world.GetPool<RotationRequest>();

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

                if (inputService.IsRotating)
                {
                    ref var rotationRequest = ref rotationRequestPool.Add(entity);
                    rotationRequest.RotationDirection = inputService.RotationDirection;
                }
            }
        }
    }
}