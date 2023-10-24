using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class TeleportationRequestConverter : IJTokenConverter
    {
        public string TokenName => nameof(CTeleportationRequest);

        public object Convert(JToken token)
        {
            return new CTeleportationRequest { };
        }
    }
}