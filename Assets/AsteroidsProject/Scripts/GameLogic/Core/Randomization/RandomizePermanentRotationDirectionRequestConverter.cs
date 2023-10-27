using AsteroidsProject.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace AsteroidsProject.GameLogic.Core
{
    public class RandomizePermanentRotationDirectionRequestConverter : IComponentConverter
    {
        public string TokenName => nameof(CRandomizePermanentRotationDirectionRequest);

        public object Convert(JToken token)
        {
            var range = JsonConvert.DeserializeObject<List<int>>(token.ToString());
            return new CRandomizePermanentRotationDirectionRequest { Range = range };
        }
    }
}