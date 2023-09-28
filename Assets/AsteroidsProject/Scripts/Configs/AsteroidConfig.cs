using AsteroidsProject.Shared;

namespace AsteroidsProject.Configs
{
    public class AsteroidConfig
    {
        public string[] PrefabAddresses { get; set; }
        public float StartingSpawns { get; set; }
        public float MaxSpawns { get; set; }
        public float MaxSpawnTime { get; set; }
    }
}