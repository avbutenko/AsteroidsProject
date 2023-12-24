using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Score
{
    public class CollectScoreRequestConverter : IComponentConverter
    {
        public string TokenName => nameof(CCollectScoreRequest);

        public object Convert(JToken token)
        {
            return new CCollectScoreRequest { Value = (int)token };
        }
    }
}