using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsteroidsProject.Test
{
    public class AsteroidConfig
    {
        public string PrefabAddress { get; set; }
        public float StartingSpawns { get; set; }
        public float MaxSpawns { get; set; }
        public float SpawnTime { get; set; }

        [JsonConverter(typeof(ComponentListJsonConverter))]
        public List<object> Components { get; set; }
    }
}