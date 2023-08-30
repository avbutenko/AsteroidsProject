﻿using AsteroidsProject.GameLogic.Features.Common;
using AsteroidsProject.GameLogic.Features.Rotation;
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

            var accelerateCommandPool = world.GetPool<AccelerationRequest>();
            var inertCommandPool = world.GetPool<InertionRequest>();
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