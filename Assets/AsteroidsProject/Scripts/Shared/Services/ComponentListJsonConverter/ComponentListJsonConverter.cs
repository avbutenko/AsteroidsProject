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
            List<object> components = new();

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


//JToken spawnSecondaryWeaponRequest = e["CSpawnSecondaryWeaponRequest"];
//JToken laserGunTag = e["CLaserGunTag"];


//if (spawnSecondaryWeaponRequest != null)
//{
//    components.Add(new CSpawnSecondaryWeaponRequest { ConfigAddress = spawnSecondaryWeaponRequest.ToString() });
//}

//////WEAPONS

//if (laserGunTag != null)
//{
//    components.Add(new CLaserGunTag { });
//}

//if (rotationDirection != null)
//{
//    Type objType = Type.GetType("AsteroidsProject.Shared.CRotationDirection, AsteroidsProject.Shared");
//    var objInstance = Activator.CreateInstance(objType);
//    objType.InvokeMember("Value", BindingFlags.SetField, null, objInstance, new object[] { (float)rotationDirection });

//    components.Add(objInstance);
//}