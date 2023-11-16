using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class UfoTagConverter : IComponentConverter
    {
        public string TokenName => nameof(CUfoTag);

        public object Convert(JToken token)
        {
            return new CUfoTag();
        }
    }
}