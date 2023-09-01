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
                              .Inc<Scale.Scale>()
                              .End();

            var positionPool = world.GetPool<Position>();
            var scalePool = world.GetPool<Scale.Scale>();

            foreach (var entity in filter)
            {
                ref var position = ref positionPool.Get(entity).Value;
                ref var scale = ref scalePool.Get(entity).Value;

                if (teleportationService.IsOutOfLevel(position, scale))
                {
                    position = teleportationService.Teleport(position, scale);
                }
            }
        }
    }
}