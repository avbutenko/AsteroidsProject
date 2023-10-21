using Newtonsoft.Json.Linq;

namespace AsteroidsProject.Shared
{
    public interface IJTokenConverter
    {
        public string TokenName { get; }
        public object Convert(JToken token);
    }
}