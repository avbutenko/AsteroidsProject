using AsteroidsProject.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace AsteroidsProject.GameLogic.Core
{
    public class RandomizeRotationSpeedRequestConverter : IJTokenConverter
    {
        public string TokenName => nameof(CRandomizeRotationSpeedRequest);

        public object Convert(JToken token)
        {
            var range = JsonConvert.DeserializeObject<List<float>>(token.ToString());
            return new CRandomizeRotationSpeedRequest { Range = range };
        }
    }
}