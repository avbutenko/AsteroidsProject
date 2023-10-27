using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Spawn.Prefab
{
    public class SpawnPrefabSystem : IEcsRunSystem
    {
        private readonly IGameObjectFactory gameObjectFactory;
        private readonly IActiveGameObjectMapService activeGameObjectMapService;

        public SpawnPrefabSystem(IGameObjectFactory gameObjectFactory, IActiveGameObjectMapService activeGameObjectMapService)
        {
            this.gameObjectFactory = gameObjectFactory;
            this.activeGameObjectMapService = activeGameObjectMapService;
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

            var link = go.GetComponent<IGameObject>();
            world.AddComponentToEntity(entity, new CGameObject { Link = link });

            activeGameObjectMapService.Add(link, new GoEntityPair { Go = go, PackedEntity = world.PackEntity(entity) });
        }
    }
}