using AsteroidsProject.GameLogic.Features.Movement;
using AsteroidsProject.Infrastructure.Services;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Teleportation
{
    public class TeleportationSystem : IEcsRunSystem
    {
        private readonly ITeleportationService teleportationService;

        public TeleportationSystem(ITeleportationService teleportationService)
        {
            this.teleportationService = teleportationService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<TeleportableTag>()
                              .Inc<Position>()
                              .End();
        }
    }
}