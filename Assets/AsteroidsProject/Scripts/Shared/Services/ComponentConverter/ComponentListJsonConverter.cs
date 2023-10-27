using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AsteroidsProject.Shared
{
    public class ComponentListJsonConverter : JsonConverter<List<object>>
    {
        public override List<object> ReadJson(JsonReader reader, Type objectType,
            List<object> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var componentConverterService = serializer.Context.Context as IComponentConverterService;
            var jsonList = JArray.Load(reader);
            var components = new List<object>();

            foreach (var e in jsonList)
            {
                foreach (var converter in componentConverterService.Converters)
                {
                    JToken token = e[converter.TokenName];

                    if (token != null)
                    {
                        components.Add(converter.Convert(token));
                    }
                }
            }

            return components;
        }

        public override void WriteJson(JsonWriter writer, List<object> value, JsonSerializer serializer) { }
    }
}