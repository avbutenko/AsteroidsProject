using AsteroidsProject.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Spawn.Obstacles.Asteroid
{
    public class SpawnAsteroidSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ISceneData sceneData;
        private readonly IConfigProvider configProvider;
        private readonly ITimeService timeService;
        private SpawnConfig asteroidSpawnconfig;
        private GameConfig gameConfig;
        private float timeToNextSpawn;

        public SpawnAsteroidSystem(IConfigProvider configProvider, ITimeService timeService, ISceneData sceneData)
        {
            this.configProvider = configProvider;
            this.timeService = timeService;
            this.sceneData = sceneData;
        }

        public async void Init(IEcsSystems systems)
        {
            gameConfig = await configProvider.Load<GameConfig>(configProvider.GameConfigPath);
            asteroidSpawnconfig = await configProvider.Load<SpawnConfig>(gameConfig.AsteroidSpawnConfigPath);
            SpawnInitialAmountOfAsteroids(systems.GetWorld());
        }

        public void Run(IEcsSystems systems)
        {
            if (asteroidSpawnconfig == null) return;

            var world = systems.GetWorld();
            var filter = world.Filter<CAsteroidTag>().End();
            var count = filter.GetEntitiesCount();

            timeToNextSpawn -= timeService.DeltaTime;

            if (timeToNextSpawn < 0 && count < asteroidSpawnconfig.MaxSpawns)
            {
                timeToNextSpawn = asteroidSpawnconfig.SpawnTime;

                Spawn(world, gameConfig.AsteroidConfigPath);
            }
        }


        private void SpawnInitialAmountOfAsteroids(EcsWorld world)
        {
            timeToNextSpawn = asteroidSpawnconfig.SpawnTime;

            var counter = asteroidSpawnconfig.StartingSpawns;

            while (counter > 0)
            {
                counter--;
                Spawn(world, gameConfig.AsteroidConfigPath);
            }
        }

        private async void Spawn(EcsWorld world, string config)
        {
            var componentList = await configProvider.Load<ComponentList>(config);
            var entity = world.NewEntityWithRawComponents(componentList.Components);
            world.AddComponentToEntity(entity, new CParent { Value = sceneData.AsteroidsPool });
            world.AddComponentToEntity(entity, new CSpawnedEntityEvent { PackedEntity = world.PackEntity(entity) });
        }
    }
}