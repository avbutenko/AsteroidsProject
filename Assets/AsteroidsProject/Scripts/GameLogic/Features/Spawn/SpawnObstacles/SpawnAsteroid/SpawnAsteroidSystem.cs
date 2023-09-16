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
        private AsteroidConfig spawnConfig;
        private float timeIntervalBetweenSpawns;
        private float timeToNextSpawn;

        public SpawnAsteroidSystem(ILevelService level, IConfigProvider configProvider, ITimeService timeService)
        {
            this.level = level;
            this.configProvider = configProvider;
            this.timeService = timeService;
        }

        public async void Init(IEcsSystems systems)
        {
            spawnConfig = await configProvider.Load<AsteroidConfig>("Configs/AsteroidSpawnConfig.json");
            timeIntervalBetweenSpawns = spawnConfig.MaxSpawnTime / (spawnConfig.MaxSpawns - spawnConfig.StartingSpawns);
            timeToNextSpawn = timeIntervalBetweenSpawns;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<AsteroidTag>().End();
            var count = filter.GetEntitiesCount();

            timeToNextSpawn -= timeService.DeltaTime;

            if (timeToNextSpawn < 0 && count < spawnConfig?.MaxSpawns)
            {
                timeToNextSpawn = timeIntervalBetweenSpawns;

                var prefabIndex = Random.Range(0, spawnConfig.PrefabAddresses.Length);

                world.NewEntityWith(new SpawnPrefab
                {
                    PrefabAddress = spawnConfig.PrefabAddresses[prefabIndex],
                    Position = level.GetRandomPosition(),
                    Rotation = Quaternion.identity,
                    Parent = null
                });
            }
        }
    }
}