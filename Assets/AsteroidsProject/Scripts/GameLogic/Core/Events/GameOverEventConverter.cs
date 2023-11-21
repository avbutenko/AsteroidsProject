using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class GameOverEventConverter : IComponentConverter
    {
        public string TokenName => nameof(CGameOverEvent);

        public object Convert(JToken token)
        {
            return new CGameOverEvent();
        }
    }
}