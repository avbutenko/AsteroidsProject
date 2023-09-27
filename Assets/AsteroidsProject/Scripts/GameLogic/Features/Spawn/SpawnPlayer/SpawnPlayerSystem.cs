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
            var config = await configProvider.Load<PlayerConfig>(gameConfig.PlayerConfigPath);

            world.NewEntityWith(new SpawnPrefabRequest
            {
                SpawnInfo = new SpawnInfo
                {
                    PrefabAddress = config.PrefabAddress,
                    Position = sceneData.SpawnPlayerPosition.position,
                    Rotation = sceneData.SpawnPlayerPosition.rotation,
                    Parent = null
                }
            });
        }
    }
}
