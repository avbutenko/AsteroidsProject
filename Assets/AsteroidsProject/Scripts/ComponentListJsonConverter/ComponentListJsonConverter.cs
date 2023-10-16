using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.GameLogic.Features.RandomizeRotationDirection;
using AsteroidsProject.GameLogic.Features.RandomizeRotationSpeed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsProject.Test
{
    public class ComponentListJsonConverter : JsonConverter<List<object>>
    {
        public override List<object> ReadJson(JsonReader reader, Type objectType,
            List<object> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonList = JArray.Load(reader);
            List<object> components = new();

            foreach (var e in jsonList)
            {
                JToken spawnPrefab = e["CSpawnPrefabRequest"];
                JToken asteroidTag = e["CAsteroidTag"];
                JToken teleportableTag = e["CTeleportableTag"];
                JToken rotation = e["CRotation"];
                JToken randomizePositionRequest = e["CRandomizePositionRequest"];
                JToken randomizeVelocityRequest = e["CRandomizeVelocityRequest"];
                JToken randomizeRotationDirectionRequest = e["CRandomizeRotationDirectionRequest"];
                JToken randomizeRotationSpeedRequest = e["CRandomizeRotationSpeedRequest"];

                if (spawnPrefab != null)
                {
                    components.Add(new CSpawnPrefabRequest { PrefabAddress = (string)spawnPrefab });
                }

                if (asteroidTag != null)
                {
                    components.Add(new CAsteroidTag());
                }

                if (teleportableTag != null)
                {
                    components.Add(new CTeleportableTag());
                }

                if (rotation != null)
                {
                    components.Add(new CRotation { Value = new Quaternion(0, 0, (float)rotation, 1) });
                }

                if (randomizePositionRequest != null)
                {
                    components.Add(new CRandomizePositionRequest());
                }

                if (randomizeVelocityRequest != null)
                {
                    var range = JsonConvert.DeserializeObject<List<Vector2>>(randomizeVelocityRequest.ToString());
                    components.Add(new CRandomizeVelocityRequest { Range = range });
                }

                if (randomizeRotationDirectionRequest != null)
                {
                    var range = JsonConvert.DeserializeObject<List<int>>(randomizeRotationDirectionRequest.ToString());
                    components.Add(new CRandomizeRotationDirectionRequest { Range = range });
                }

                if (randomizeRotationSpeedRequest != null)
                {
                    var range = JsonConvert.DeserializeObject<List<float>>(randomizeRotationSpeedRequest.ToString());
                    components.Add(new CRandomizeRotationSpeedRequest { Range = range });
                }



                //if (rotationDirection != null)
                //{
                //    Type objType = Type.GetType("AsteroidsProject.Shared.CRotationDirection, AsteroidsProject.Shared");
                //    var objInstance = Activator.CreateInstance(objType);
                //    objType.InvokeMember("Value", BindingFlags.SetField, null, objInstance, new object[] { (float)rotationDirection });

                //    components.Add(objInstance);
                //}

                //if (rotationSpeed != null)
                //{
                //    Type objType = Type.GetType("AsteroidsProject.Shared.CRotationSpeed, AsteroidsProject.Shared");
                //    var objInstance = Activator.CreateInstance(objType);
                //    objType.InvokeMember("Value", BindingFlags.SetField, null, objInstance, new object[] { (float)rotationSpeed });

                //    components.Add(objInstance);
                //}
            }
            return components;
        }

        public override void WriteJson(JsonWriter writer, List<object> value, JsonSerializer serializer)
        {

        }
    }
}