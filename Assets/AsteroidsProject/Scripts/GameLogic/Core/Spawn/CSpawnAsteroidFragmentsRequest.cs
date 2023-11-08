using Assets.AsteroidsProject.Scripts.Shared;
using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Core
{
    public struct CSpawnAsteroidFragmentsRequest : IHaveConfigAddress, IHavePosition
    {
        public string Config;
        public int Amount;
        public Vector2 SpawnPosition;

        public string ConfigAddress
        {
            get => Config;
            set => Config = value;
        }

        public Vector2 Position
        {
            get => SpawnPosition;
            set => SpawnPosition = value;
        }
    }
}