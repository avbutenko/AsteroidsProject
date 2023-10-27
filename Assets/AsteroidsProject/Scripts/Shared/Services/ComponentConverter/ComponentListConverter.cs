using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace AsteroidsProject.Shared
{
    public abstract class ComponentListConverter<T> : IComponentConverter where T : struct, IHaveComponentList
    {
        private readonly IComponentConverterService componentConverterService;

        public ComponentListConverter(IComponentConverterService componentConverterService)
        {
            this.componentConverterService = componentConverterService;
        }

        public string TokenName => typeof(T).Name;

        public object Convert(JToken token)
        {
            var serializerSettings = GetSerializerSettings();
            var list = JsonConvert.DeserializeObject<ComponentList>(token.ToString(), serializerSettings);
            return new T { ComponentList = list.Components };
        }

        private JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                Context = new StreamingContext(StreamingContextStates.All, componentConverterService)
            };
        }
    }
}