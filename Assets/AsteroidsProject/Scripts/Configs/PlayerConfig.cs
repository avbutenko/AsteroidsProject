using AsteroidsProject.Test;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsteroidsProject.Configs
{
    public class PlayerConfig
    {
        [JsonConverter(typeof(ComponentListJsonConverter))]
        public List<object> Components { get; set; }
    }
}