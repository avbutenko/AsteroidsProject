using AsteroidsProject.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsteroidsProject.GameLogic.Features.Events.OnAttack
{
    public struct COnAttack : IHaveComponentList
    {
        [JsonConverter(typeof(ComponentListJsonConverter))]
        public List<object> Components;
        public List<object> ComponentList { set => Components = value; }
    }
}