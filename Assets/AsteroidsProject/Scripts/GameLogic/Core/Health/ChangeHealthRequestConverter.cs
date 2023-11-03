using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class ChangeHealthRequestConverter : IComponentConverter
    {
        public string TokenName => nameof(CChangeHealthRequest);

        public object Convert(JToken token)
        {
            return new CChangeHealthRequest { Value = (int)token };
        }
    }
}