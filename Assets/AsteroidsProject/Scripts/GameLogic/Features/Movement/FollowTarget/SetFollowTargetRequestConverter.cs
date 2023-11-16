using AsteroidsProject.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Movement.FollowTarget
{
    public class SetFollowTargetRequestConverter : IComponentConverter
    {
        public string TokenName => nameof(CSetFollowTargetRequest);

        public object Convert(JToken token)
        {
            var data = JsonConvert.DeserializeObject<CSetFollowTargetRequest>(token.ToString());
            return new CSetFollowTargetRequest { TargetComponent = data.TargetComponent };
        }
    }
}