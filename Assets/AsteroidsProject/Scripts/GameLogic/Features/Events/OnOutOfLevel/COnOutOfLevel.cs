using AsteroidsProject.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsteroidsProject.GameLogic.Features.Events.OnOutOfLevel
{
    public struct COnOutOfLevel : IHaveComponentList
    {
        [JsonConverter(typeof(ComponentListJsonConverter))]
        public List<object> AddToSelfComponents;
        public List<object> ComponentList { set => AddToSelfComponents = value; }
    }
}