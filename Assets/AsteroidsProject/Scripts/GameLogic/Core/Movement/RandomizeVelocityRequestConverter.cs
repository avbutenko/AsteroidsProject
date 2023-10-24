using AsteroidsProject.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Core
{
    public class RandomizeVelocityRequestConverter : IJTokenConverter
    {
        public string TokenName => nameof(CRandomizeVelocityRequest);

        public object Convert(JToken token)
        {
            var range = JsonConvert.DeserializeObject<List<Vector2>>(token.ToString());
            return new CRandomizeVelocityRequest { Range = range };
        }
    }
}