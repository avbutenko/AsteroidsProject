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

        private AsteroidConfig config;
        private float timeToNextSpawn;
        private EcsWorld world;

        public SpawnAsteroidTimerSystem(IConfigProvider configProvider, ITimeService timeService)
        {
            this.configProvider = configProvider;
            this.timeService = timeService;
        }

        public async void Init(IEcsSystems systems)
        {
            var gameConfig = await configProvider.Load<GameConfig>("Configs/GameConfig.json");
            config = await configProvider.Load<AsteroidConfig>(gameConfig.AsteroidConfigPath);
            timeToNextSpawn = config.SpawnTime;

            world = systems.GetWorld();

            var counter = config.StartingSpawns;
            while (counter > 0)
            {
                counter--;
                world.NewEntityWith(new SpawnAsteroidRequest());
            }
        }

        public void Run(IEcsSystems systems)
        {
            var filter = world.Filter<AsteroidTag>().End();
            var count = filter.GetEntitiesCount();

            timeToNextSpawn -= timeService.DeltaTime;

            if (timeToNextSpawn < 0 && count >= config?.StartingSpawns && count < config?.MaxSpawns)
            {
                timeToNextSpawn = config.SpawnTime;

                world.NewEntityWith(new SpawnAsteroidRequest());
            }
        }
    }
}