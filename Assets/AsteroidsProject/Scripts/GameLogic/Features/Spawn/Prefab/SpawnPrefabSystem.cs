using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

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
        var filter = world.Filter<CSpawnPrefabRequest>().End();
        var spawnPool = world.GetPool<CSpawnPrefabRequest>();
        var positionPool = world.GetPool<CPosition>();
        var rotationPool = world.GetPool<CRotation>();
        var parentPool = world.GetPool<CParent>();

        foreach (var entity in filter)
        {
            ref var prefabAddress = ref spawnPool.Get(entity).PrefabAddress;
            ref var position = ref positionPool.Get(entity).Value;
            ref var rotation = ref rotationPool.Get(entity).Value;

            var spawnInfo = new SpawnInfo
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
            spawnPool.Del(entity);
        }
    }

    private async void Spawn(int entity, EcsWorld world, SpawnInfo info)
    {
        var go = await gameObjectFactory.CreateAsync(info);

        var link = go.GetComponent<IGameObject>();
        world.AddComponentToEntity(entity, new CGameObject { Link = link });

        activeGameObjectMapService.Add(link, new GoEntityPair { Go = go, PackedEntity = world.PackEntity(entity) });
    }
}