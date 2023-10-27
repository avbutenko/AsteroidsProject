using Newtonsoft.Json.Linq;

namespace AsteroidsProject.Shared
{
    public interface IComponentConverter
    {
        public string TokenName { get; }
        public object Convert(JToken token);
    }
}