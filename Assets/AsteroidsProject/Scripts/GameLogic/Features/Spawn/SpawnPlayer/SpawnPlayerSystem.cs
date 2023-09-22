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
        private readonly IGameObjectFactory factory;

        public SpawnPlayerSystem(ISceneData sceneData, IConfigProvider configProvider, IGameObjectFactory factory)
        {
            this.sceneData = sceneData;
            this.configProvider = configProvider;
            this.factory = factory;
        }

        public async void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var config = await configProvider.Load<PlayerConfig>("Configs/PlayerConfig.json");

            world.NewEntityWith(new SpawnPrefab
            {
                PrefabAddress = config.PrefabAddress,
                Position = sceneData.SpawnPlayerPosition.position,
                Rotation = sceneData.SpawnPlayerPosition.rotation,
                Parent = null
            });
        }
    }
}
