using Assets.AsteroidsProject.Scripts.Configs;
using AsteroidsProject.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.SpawnAsteroid
{
    public class SpawnAsteroidTimerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IConfigProvider configProvider;
        private readonly ITimeService timeService;

        private AsteroidSpawnConfig config;
        private float timeToNextSpawn;
        private EcsWorld world;

        public SpawnAsteroidTimerSystem(IConfigProvider configProvider, ITimeService timeService)
        {
            this.configProvider = configProvider;
            this.timeService = timeService;
        }

        public async void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            var gameConfig = await configProvider.Load<GameConfig>("Configs/GameConfig.json");
            config = await configProvider.Load<AsteroidSpawnConfig>(gameConfig.AsteroidSpawnConfigPath);

            timeToNextSpawn = config.SpawnTime;

            var counter = config.StartingSpawns;
            while (counter > 0)
            {
                counter--;
                world.NewEntityWith(new CSpawnAsteroidRequest());
            }
        }

        public void Run(IEcsSystems systems)
        {

            if (config == null)
            {
                return;
            }

            var filter = world.Filter<CAsteroidTag>().End();
            var count = filter.GetEntitiesCount();

            timeToNextSpawn -= timeService.DeltaTime;

            if (timeToNextSpawn < 0 && count < config.MaxSpawns)
            {
                timeToNextSpawn = config.SpawnTime;

                world.NewEntityWith(new CSpawnAsteroidRequest());
            }
        }
    }
}