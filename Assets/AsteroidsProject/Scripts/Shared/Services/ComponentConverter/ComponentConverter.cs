using Newtonsoft.Json;
using System;

namespace Assets.AsteroidsProject.Scripts.Shared
{
    public class ComponentConverter<T> : JsonConverter<T> where T : struct
    {
        public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var result = (T)Activator.CreateInstance(typeof(T));
            return result;
        }

        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer) { }
    }
}