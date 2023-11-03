using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Spawn.Prefab
{
    public class SpawnPrefabSystem : IEcsRunSystem
    {
        private readonly IGameObjectFactory gameObjectFactory;
        private readonly IActiveGOMappingService activeGOMappingService;

        public SpawnPrefabSystem(IGameObjectFactory gameObjectFactory, IActiveGOMappingService activeGOMappingService)
        {
            this.gameObjectFactory = gameObjectFactory;
            this.activeGOMappingService = activeGOMappingService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var filter = world.Filter<CSpawnPrefabRequest>()
                              .Inc<CPrefabAddress>()
                              .Inc<CPosition>()
                              .Inc<CRotation>()
                              .End();

            var requestPool = world.GetPool<CSpawnPrefabRequest>();
            var prefabAddressPool = world.GetPool<CPrefabAddress>();
            var positionPool = world.GetPool<CPosition>();
            var rotationPool = world.GetPool<CRotation>();
            var parentPool = world.GetPool<CParent>();

            foreach (var entity in filter)
            {
                ref var prefabAddress = ref prefabAddressPool.Get(entity).Value;
                ref var position = ref positionPool.Get(entity).Value;
                ref var rotation = ref rotationPool.Get(entity).Value;

                var spawnInfo = new SpawnPrefabInfo
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

        private async void Spawn(int entity, EcsWorld world, SpawnPrefabInfo info)
        {
            var go = await gameObjectFactory.CreateAsync(info);
            var goID = go.GetInstanceID();

            world.AddComponentToEntity(entity, new CGameObjectInstanceID { Value = goID });
            activeGOMappingService.Add(goID, new GoEntityPair { Go = go, PackedEntity = world.PackEntity(entity) });
        }
    }
}