using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn
{
    public class SpawnAsteroidSystem : IEcsInitSystem, IEcsRunSystem, IEcsPostDestroySystem
    {
        private readonly IGameConfigProvider configProvider;
        private readonly IConfigLoader configLoader;
        private readonly ITimeService timeService;
        private SpawnConfig asteroidSpawnconfig;
        private GameSceneConfig gameSceneConfig;
        private float timeToNextSpawn;
        private EcsWorld world;
        private EcsFilter filter;
        private GameObject parentGO;

        public SpawnAsteroidSystem(IGameConfigProvider configProvider, IConfigLoader configLoader, ITimeService timeService)
        {
            this.configProvider = configProvider;
            this.configLoader = configLoader;
            this.timeService = timeService;
        }

        public async void Init(IEcsSystems systems)
        {
            parentGO = new GameObject("Asteroids");
            gameSceneConfig = await configLoader.Load<GameSceneConfig>(configProvider.GameConfig.ScenesConfig.GameSceneConfigLabel);
            asteroidSpawnconfig = await configLoader.Load<SpawnConfig>(gameSceneConfig.AsteroidSpawnConfigLabel);
            world = systems.GetWorld();
            filter = world.Filter<CAsteroidTag>().End();
            SpawnInitialAmountOfAsteroids();
        }

        public void Run(IEcsSystems systems)
        {
            if (asteroidSpawnconfig == null) return;

            var count = filter.GetEntitiesCount();

            timeToNextSpawn -= timeService.DeltaTime;

            if (timeToNextSpawn < 0 && count < asteroidSpawnconfig.MaxSpawns)
            {
                timeToNextSpawn = asteroidSpawnconfig.SpawnTime;

                Spawn(gameSceneConfig.AsteroidConfigLabel);
            }
        }

        private void SpawnInitialAmountOfAsteroids()
        {
            timeToNextSpawn = asteroidSpawnconfig.SpawnTime;

            var counter = asteroidSpawnconfig.StartingSpawns;

            while (counter > 0)
            {
                counter--;
                Spawn(gameSceneConfig.AsteroidConfigLabel);
            }
        }

        private async void Spawn(string config)
        {
            var componentList = await configLoader.Load<ComponentList>(config);
            var entity = world.NewEntityWithRawComponents(componentList.Components);
            world.AddComponentToEntity(entity, new CParent { Value = parentGO.transform });
            world.AddComponentToEntity(entity, new CSpawnedEntityEvent { PackedEntity = world.PackEntity(entity) });
        }

        public void PostDestroy(IEcsSystems systems)
        {
            Object.Destroy(parentGO);
        }
    }
}