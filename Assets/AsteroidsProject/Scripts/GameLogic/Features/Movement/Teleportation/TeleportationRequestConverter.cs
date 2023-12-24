using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class TeleportationRequestConverter : IComponentConverter
    {
        public string TokenName => nameof(CTeleportationRequest);

        public object Convert(JToken token)
        {
            return new CTeleportationRequest { };
        }
    }
}