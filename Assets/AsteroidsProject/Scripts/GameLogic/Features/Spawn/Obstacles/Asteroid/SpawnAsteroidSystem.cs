using AsteroidsProject.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Spawn.Obstacles.Asteroid
{
    public class SpawnAsteroidSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IGameSceneData sceneData;
        private readonly IConfigProvider configProvider;
        private readonly ITimeService timeService;
        private SpawnConfig asteroidSpawnconfig;
        private GameSceneConfig gameSceneConfig;
        private float timeToNextSpawn;
        private EcsWorld world;
        private EcsFilter filter;

        public SpawnAsteroidSystem(IConfigProvider configProvider, ITimeService timeService, IGameSceneData sceneData)
        {
            this.configProvider = configProvider;
            this.timeService = timeService;
            this.sceneData = sceneData;
        }

        public async void Init(IEcsSystems systems)
        {
            gameSceneConfig = await configProvider.Load<GameSceneConfig>(sceneData.SceneConfigAssetPath);
            asteroidSpawnconfig = await configProvider.Load<SpawnConfig>(gameSceneConfig.AsteroidSpawnConfigPath);
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

                Spawn(gameSceneConfig.AsteroidConfigPath);
            }
        }

        private void SpawnInitialAmountOfAsteroids()
        {
            timeToNextSpawn = asteroidSpawnconfig.SpawnTime;

            var counter = asteroidSpawnconfig.StartingSpawns;

            while (counter > 0)
            {
                counter--;
                Spawn(gameSceneConfig.AsteroidConfigPath);
            }
        }

        private async void Spawn(string config)
        {
            var componentList = await configProvider.Load<ComponentList>(config);
            var entity = world.NewEntityWithRawComponents(componentList.Components);
            world.AddComponentToEntity(entity, new CParent { Value = sceneData.AsteroidsPool });
            world.AddComponentToEntity(entity, new CSpawnedEntityEvent { PackedEntity = world.PackEntity(entity) });
        }
    }
}