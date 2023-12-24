using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class UpdateGameObjectViewRotationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IActiveGOMappingService activeGOMappingService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CGameObjectInstanceID> goIDPool;
        private EcsPool<CRotation> rotationPool;

        public UpdateGameObjectViewRotationSystem(IActiveGOMappingService activeGOMappingService)
        {
            this.activeGOMappingService = activeGOMappingService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CGameObjectInstanceID>()
                              .Inc<CRotation>()
                              .End();

            goIDPool = world.GetPool<CGameObjectInstanceID>();
            rotationPool = world.GetPool<CRotation>();
        }

        public void Run(IEcsSystems systems)
        {
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