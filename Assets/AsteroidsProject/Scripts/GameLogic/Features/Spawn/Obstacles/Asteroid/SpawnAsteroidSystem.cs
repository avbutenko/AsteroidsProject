using AsteroidsProject.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.SpawnAsteroid
{
    public class SpawnAsteroidSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IConfigProvider configProvider;

        private ComponentList config;

        public SpawnAsteroidSystem(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        public async void Init(IEcsSystems systems)
        {
            var gameConfig = await configProvider.Load<GameConfig>("Configs/GameConfig.json");
            config = await configProvider.Load<ComponentList>(gameConfig.AsteroidConfigPath);
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CSpawnAsteroidRequest>().End();

            foreach (var entity in filter)
            {
                var asteroidEntity = world.NewEntity();
                foreach (var component in config.Components)
                {
                    world.AddRawComponentToEntity(asteroidEntity, component);
                }

                world.DelEntity(entity);
            }
        }
    }
}