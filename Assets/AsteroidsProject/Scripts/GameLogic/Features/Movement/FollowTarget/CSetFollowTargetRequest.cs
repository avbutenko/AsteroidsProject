using Assets.AsteroidsProject.Scripts.Shared;
using Newtonsoft.Json;

namespace AsteroidsProject.GameLogic.Features.Movement.FollowTarget
{
    public struct CSetFollowTargetRequest
    {
        [JsonConverter(typeof(ComponentConverter<CSetFollowTargetRequest>))]
        public object TargetComponent;
    }
}