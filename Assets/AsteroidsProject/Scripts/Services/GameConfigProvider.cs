using AsteroidsProject.Shared;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.Services
{
    public class GameConfigProvider : IGameConfigProvider, IInitializable
    {
        private GameConfig gameConfig;

        public GameConfig GameConfig => gameConfig;

        public void Initialize()
        {
            var configAsset = Resources.Load<TextAsset>("GameConfig");
            gameConfig = JsonConvert.DeserializeObject<GameConfig>(configAsset.text);
        }
    }
}