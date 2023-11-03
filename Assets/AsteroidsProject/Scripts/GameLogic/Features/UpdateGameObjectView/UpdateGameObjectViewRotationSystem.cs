using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.UpdateGameObjectView
{
    public class UpdateGameObjectViewRotationSystem : IEcsRunSystem
    {
        private readonly IActiveGOMappingService activeGOMappingService;

        public UpdateGameObjectViewRotationSystem(IActiveGOMappingService activeGOMappingService)
        {
            this.activeGOMappingService = activeGOMappingService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CGameObjectInstanceID>()
                              .Inc<CRotation>()
                              .End();

            var goIDPool = world.GetPool<CGameObjectInstanceID>();
            var rotationPool = world.GetPool<CRotation>();


            foreach (var entity in filter)
            {
                ref var rotation = ref rotationPool.Get(entity).Value;
                ref var goID = ref goIDPool.Get(entity).Value;
                if (!activeGOMappingService.TryGetGoLink(goID, out var goLink)) return;

                goLink.Rotation = rotation;
            }
        }
    }
}