using AsteroidsProject.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsteroidsProject.GameLogic.Features.Events.OnSpawn
{
    public struct COnSpawn : IHaveComponentList
    {
        [JsonConverter(typeof(ComponentListJsonConverter))]
        public List<object> Components;
        public List<object> ComponentList { set => Components = value; }
    }
}