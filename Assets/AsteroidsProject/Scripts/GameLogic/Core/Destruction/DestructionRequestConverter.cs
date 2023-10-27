using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;


namespace AsteroidsProject.GameLogic.Core
{
    public class DestructionRequestConverter : IComponentConverter
    {
        public string TokenName => nameof(CDestructionRequest);

        public object Convert(JToken token)
        {
            return new CDestructionRequest();
        }
    }
}