using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsteroidsProject.Shared
{
    public class ComponentList
    {
        [JsonConverter(typeof(ComponentListJsonConverter))]
        public List<object> Components { get; set; }
    }
}