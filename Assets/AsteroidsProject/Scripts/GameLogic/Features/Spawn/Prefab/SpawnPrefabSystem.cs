using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

public class SpawnPrefabSystem : IEcsRunSystem
{
    private readonly IGameObjectFactory gameObjectFactory;

    public SpawnPrefabSystem(IGameObjectFactory gameObjectFactory)
    {
        this.gameObjectFactory = gameObjectFactory;
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

            if (parentPool.Has(entity))
            {
                ref var parent = ref parentPool.Get(entity).Value;

                Spawn(entity, world, new SpawnInfo
                {
                    PrefabAddress = prefabAddress,
                    Position = position,
                    Rotation = rotation,
                    Parent = parent,
                });

                parentPool.Del(entity);
            }
            else
            {
                Spawn(entity, world, new SpawnInfo
                {
                    PrefabAddress = prefabAddress,
                    Position = position,
                    Rotation = rotation,
                });
            }

            spawnPool.Del(entity);
        }
    }

    private async void Spawn(int entity, EcsWorld world, SpawnInfo info)
    {
        var go = await gameObjectFactory.CreateAsync(info);

        var goLink = go.GetComponent<ILinkToGameObject>();
        goLink.Entity = world.PackEntity(entity);
        world.AddComponentToEntity(entity, new CLinkToGameObject { View = goLink });
    }
}