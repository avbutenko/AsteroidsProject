using AsteroidsProject.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.SpawnAsteroid
{
    public class SpawnAsteroidSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ILevelService level;
        private readonly IConfigProvider configProvider;
        private readonly ITimeService timeService;
        private readonly ISceneData sceneData;
        private readonly IAssetProvider assetProvider;

        private AsteroidConfig config;
        private float timeIntervalBetweenSpawns;
        private float timeToNextSpawn;

        public SpawnAsteroidSystem(ILevelService level, IConfigProvider configProvider, ITimeService timeService,
            ISceneData sceneData, IAssetProvider assetProvider)
        {
            this.level = level;
            this.configProvider = configProvider;
            this.timeService = timeService;
            this.sceneData = sceneData;
            this.assetProvider = assetProvider;
        }

        public async void Init(IEcsSystems systems)
        {
            var gameConfig = await configProvider.Load<GameConfig>("Configs/GameConfig.json");
            config = await configProvider.Load<AsteroidConfig>(gameConfig.AsteroidConfigPath);
            timeIntervalBetweenSpawns = config.MaxSpawnTime / (config.MaxSpawns - config.StartingSpawns);
            timeToNextSpawn = timeIntervalBetweenSpawns;
        }

        public async void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<AsteroidTag>().End();
            var count = filter.GetEntitiesCount();

            timeToNextSpawn -= timeService.DeltaTime;

            if (timeToNextSpawn < 0 && count < config?.MaxSpawns)
            {
                timeToNextSpawn = timeIntervalBetweenSpawns;

                var asteroidEntity = world.NewEntity();
                var prefabIndex = Random.Range(0, config.PrefabAddresses.Length);
                var position = level.GetRandomPosition();
                var rotation = Quaternion.identity;
                var velocityX = Random.Range(config.VelocityX[0], config.VelocityX[1]);
                var velocityY = Random.Range(config.VelocityY[0], config.VelocityY[1]);
                var rotationDirection = Random.Range(config.RotationDirection[0], config.RotationDirection[1]);
                var rotationSpeed = Random.Range(config.RotationSpeed[0], config.RotationSpeed[1]);

                world.AddComponentToEntity(asteroidEntity, new AsteroidTag { });
                world.AddComponentToEntity(asteroidEntity, new TeleportableTag { });
                world.AddComponentToEntity(asteroidEntity, new Position { Value = position });
                world.AddComponentToEntity(asteroidEntity, new Rotation { Value = rotation });
                world.AddComponentToEntity(asteroidEntity, new Velocity { Value = new Vector2(velocityX, velocityY) });
                world.AddComponentToEntity(asteroidEntity, new RotationDirection { Value = rotationDirection });
                world.AddComponentToEntity(asteroidEntity, new RotationSpeed { Value = rotationSpeed });

                var prefab = await assetProvider.Load<GameObject>(config.PrefabAddresses[prefabIndex]);
                var go = Object.Instantiate(prefab, position, rotation, sceneData.AsteroidsParent);
                var goLink = go.GetComponent<ILinkToGameObject>();
                goLink.Entity = world.PackEntity(asteroidEntity);
                world.AddComponentToEntity(asteroidEntity, new LinkToGameObject { View = goLink });
            }
        }
    }
}