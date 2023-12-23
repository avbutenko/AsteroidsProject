using AsteroidsProject.Shared;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.Services
{
    public class GameConfigProvider : IGameConfigProvider, IInitializable
    {
        private readonly IAssetProvider assetProvider;
        private GameConfig gameConfig;

        public GameConfigProvider(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public GameConfig GameConfig => gameConfig;

        public void Initialize()
        {
            var configAsset = assetProvider.ResourceLoad<TextAsset>("GameConfig");
            gameConfig = JsonConvert.DeserializeObject<GameConfig>(configAsset.text);
        }
    }
}