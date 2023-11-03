using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.UpdateGameObjectView
{
    public class UpdateGameObjectViewPositionSystem : IEcsRunSystem
    {
        private readonly IActiveGOMappingService activeGOMappingService;

        public UpdateGameObjectViewPositionSystem(IActiveGOMappingService activeGOMappingService)
        {
            this.activeGOMappingService = activeGOMappingService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CGameObjectInstanceID>()
                              .Inc<CPosition>()
                              .End();

            var goIDPool = world.GetPool<CGameObjectInstanceID>();
            var positionPool = world.GetPool<CPosition>();


            foreach (var entity in filter)
            {
                ref var position = ref positionPool.Get(entity).Value;
                ref var goID = ref goIDPool.Get(entity).Value;
                if (!activeGOMappingService.TryGetGoLink(goID, out var goLink)) return;

                goLink.Position = position;
            }
        }
    }
}