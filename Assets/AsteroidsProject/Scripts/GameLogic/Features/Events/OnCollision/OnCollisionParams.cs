using AsteroidsProject.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsteroidsProject.GameLogic.Features.Events.OnCollision
{
    public struct OnCollisionParams
    {
        [JsonConverter(typeof(ComponentListJsonConverter))]
        public List<object> RelevantForCollisionTags;

        [JsonConverter(typeof(ComponentListJsonConverter))]
        public List<object> AddToSelfComponents;

        [JsonConverter(typeof(ComponentListJsonConverter))]
        public List<object> AddToCollidedObjectComponents;
    }
}