using AsteroidsProject.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Spawn.Player
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
            var gameConfig = await configProvider.Load<GameConfig>(configProvider.GameConfigPath);
            Spawn(world, gameConfig.PlayerConfigPath);
        }

        private async void Spawn(EcsWorld world, string config)
        {
            var componentList = await configProvider.Load<ComponentList>(config);
            var components = componentList.Components;
            components.Add(new CPosition { Value = sceneData.SpawnPlayerPoint.position });
            components.Add(new CRotation { Value = sceneData.SpawnPlayerPoint.rotation });

            world.NewEntityWithComponents(componentList.Components);
        }
    }
}