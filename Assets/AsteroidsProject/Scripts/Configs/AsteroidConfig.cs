using AsteroidsProject.Shared;

namespace AsteroidsProject.Configs
{
    public class AsteroidConfig : IPrefabConfig
    {
        public string[] PrefabAddresses { get; set; }
        public float StartingSpawns { get; set; }
        public float MaxSpawns { get; set; }
        public float MaxSpawnTime { get; set; }
        public VelocityRange VelocityRange { get; set; }
    }

    public struct VelocityRange
    {
        public float[] XRange;
        public float[] YRange;
    }
}