using AsteroidsProject.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Core
{
    public class VelocityConverter : IJTokenConverter
    {
        public string TokenName => nameof(CVelocity);

        public object Convert(JToken token)
        {
            var value = JsonConvert.DeserializeObject<Vector2>(token.ToString());
            return new CVelocity { Value = value };
        }
    }
}