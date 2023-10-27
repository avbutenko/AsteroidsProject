using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Spawn.Prefab
{
    public class PrefabAddressConverter : IComponentConverter
    {
        public string TokenName => nameof(CPrefabAddress);

        public object Convert(JToken token)
        {
            return new CPrefabAddress { Value = (string)token };
        }
    }
}