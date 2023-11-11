using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Score
{
    public class ScoreConverter : IComponentConverter
    {
        public string TokenName => nameof(CScore);

        public object Convert(JToken token)
        {
            return new CScore { Value = (int)token };
        }
    }
}