using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class SetFollowTargetRequestConverter : IComponentConverter
    {
        private readonly IComponentConverterService componentConverterService;
        public SetFollowTargetRequestConverter(IComponentConverterService componentConverterService)
        {
            this.componentConverterService = componentConverterService;
        }
        public string TokenName => nameof(CSetFollowTargetRequest);

        public object Convert(JToken token)
        {
            object result = null;

            foreach (var converter in componentConverterService.Converters)
            {
                JToken token2 = token[converter.TokenName];

                if (token2 != null)
                {
                    result = converter.Convert(token2);
                    break;
                }
            }

            return new CSetFollowTargetRequest { TargetComponent = result };
        }
    }
}