using AsteroidsProject.Test;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Assets.AsteroidsProject.Scripts.Configs
{
    public class ComponentListConfig
    {
        [JsonConverter(typeof(ComponentListJsonConverter))]
        public List<object> Components { get; set; }
    }
}