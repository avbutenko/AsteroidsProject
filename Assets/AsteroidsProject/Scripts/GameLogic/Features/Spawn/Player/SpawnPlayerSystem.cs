using AsteroidsProject.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.SpawnPlayer
{
    public class SpawnPlayerSystem : IEcsInitSystem
    {
        private readonly ISceneData sceneData;
        private readonly IConfigProvider configProvider;

        public SpawnPlayerSystem(ISceneData sceneData, IConfigProvider configProvider)
        {
            this.sceneData = sceneData;
            this.configProvider = configProvider;
        }

        public async void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var gameConfig = await configProvider.Load<GameConfig>("Configs/GameConfig.json");
            var config = await configProvider.Load<ComponentList>(gameConfig.PlayerConfigPath);

            var playerEntity = world.NewEntity();

            foreach (var component in config.Components)
            {
                world.AddRawComponentToEntity(playerEntity, component);
            }

            world.AddComponentToEntity(playerEntity, new CPosition { Value = sceneData.SpawnPlayerPosition.position });
            world.AddComponentToEntity(playerEntity, new CRotation { Value = sceneData.SpawnPlayerPosition.rotation });
        }
    }
}