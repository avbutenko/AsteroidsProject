using AsteroidsProject.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsteroidsProject.GameLogic.Core
{
    public struct COnSpawn
    {
        [JsonConverter(typeof(ComponentListJsonConverter))]
        public List<object> Components;
    }
}