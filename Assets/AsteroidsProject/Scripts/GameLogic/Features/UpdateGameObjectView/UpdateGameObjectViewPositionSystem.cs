using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.UpdateGameObjectView
{
    public class UpdateGameObjectViewPositionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IActiveGOMappingService activeGOMappingService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CGameObjectInstanceID> goIDPool;
        private EcsPool<CPosition> positionPool;

        public UpdateGameObjectViewPositionSystem(IActiveGOMappingService activeGOMappingService)
        {
            this.activeGOMappingService = activeGOMappingService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CGameObjectInstanceID>()
                              .Inc<CPosition>()
                              .End();

            goIDPool = world.GetPool<CGameObjectInstanceID>();
            positionPool = world.GetPool<CPosition>();
        }

        public void Run(IEcsSystems systems)
        {
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