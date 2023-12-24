using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class BulletTagConverter : IComponentConverter
    {
        public string TokenName => nameof(CBulletTag);

        public object Convert(JToken token)
        {
            return new CBulletTag();
        }
    }
}