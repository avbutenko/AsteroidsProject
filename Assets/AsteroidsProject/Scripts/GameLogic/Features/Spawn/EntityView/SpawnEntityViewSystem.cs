using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Spawn.EntityView
{
    public class SpawnEntityViewSystem : IEcsRunSystem
    {
        private readonly IGameObjectFactory gameObjectFactory;
        private readonly IActiveGOMappingService activeGOMappingService;

        public SpawnEntityViewSystem(IGameObjectFactory gameObjectFactory, IActiveGOMappingService activeGOMappingService)
        {
            this.gameObjectFactory = gameObjectFactory;
            this.activeGOMappingService = activeGOMappingService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var filter = world.Filter<CSpawnEntityViewRequest>()
                              .Inc<CPosition>()
                              .Inc<CRotation>()
                              .End();

            var requestPool = world.GetPool<CSpawnEntityViewRequest>();
            var positionPool = world.GetPool<CPosition>();
            var rotationPool = world.GetPool<CRotation>();
            var parentPool = world.GetPool<CParent>();

            foreach (var entity in filter)
            {
                ref var prefabAddress = ref requestPool.Get(entity).PrefabAddress;
                ref var position = ref positionPool.Get(entity).Value;
                ref var rotation = ref rotationPool.Get(entity).Value;

                var spawnInfo = new SpawnEntityViewInfo
                {
                    PrefabAddress = prefabAddress,
                    Position = position,
                    Rotation = rotation,
                };

                if (parentPool.Has(entity))
                {
                    ref var parent = ref parentPool.Get(entity).Value;
                    spawnInfo.Parent = parent;
                    parentPool.Del(entity);
                }

                Spawn(entity, world, spawnInfo);
                requestPool.Del(entity);
            }
        }

        private async void Spawn(int entity, EcsWorld world, SpawnEntityViewInfo info)
        {
            var go = await gameObjectFactory.CreateAsync(info);
            var goID = go.GetInstanceID();

            world.AddComponentToEntity(entity, new CGameObjectInstanceID { Value = goID });
            activeGOMappingService.Add(goID, new GoEntityPair { Go = go, PackedEntity = world.PackEntity(entity) });
        }
    }
}