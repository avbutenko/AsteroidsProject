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



//JToken teleportRequest = e["CTeleportRequest"];
////JToken onOutOfLevelTeleportTag = e["COnOutOfLevelTeleportTag"];
////JToken onOutOfLevelDestroyTag = e["COnOutOfLevelDestroyTag"];
//JToken onOutOfLevel = e["COnOutOfLevel"];
//JToken rotation = e["CRotation"];
//JToken randomizePositionRequest = e["CRandomizePositionRequest"];
//JToken randomizeVelocityRequest = e["CRandomizeVelocityRequest"];
//JToken randomizePermanentRotationDirectionRequest = e["CRandomizePermanentRotationDirectionRequest"];
//JToken randomizeRotationSpeedRequest = e["CRandomizeRotationSpeedRequest"];
//JToken rotationSpeed = e["CRotationSpeed"];
//JToken accelerationModifier = e["CAccelerationModifier"];
//JToken deaccelerationModifier = e["CDeaccelerationModifier"];
//JToken maxVelocityMagnitude = e["CMaxVelocityMagnitude"];
//JToken spawnPrimaryWeaponRequest = e["CSpawnPrimaryWeaponRequest"];
//JToken spawnSecondaryWeaponRequest = e["CSpawnSecondaryWeaponRequest"];
//JToken coolDown = e["CCoolDown"];
//JToken bulletGunTag = e["CBulletGunTag"];
//JToken laserGunTag = e["CLaserGunTag"];
//JToken bulletTag = e["CBulletTag"];




////if (onOutOfLevelTeleportTag != null)
////{
////    components.Add(new COnOutOfLevelTeleportTag());
////}

//if (teleportRequest != null)
//{
//    components.Add(new CTeleportRequest());
//}

////if (onOutOfLevelDestroyTag != null)
////{
////    components.Add(new COnOutOfLevelDestroyTag());
////}

////if (rotation != null)
////{
////    components.Add(new CRotation { Value = new Quaternion(0, 0, (float)rotation, 1) });
////}

//if (randomizePositionRequest != null)
//{
//    components.Add(new CRandomizePositionRequest());
//}

//if (randomizeVelocityRequest != null)
//{
//    var range = JsonConvert.DeserializeObject<List<Vector2>>(randomizeVelocityRequest.ToString());
//    components.Add(new CRandomizeVelocityRequest { Range = range });
//}

//if (randomizePermanentRotationDirectionRequest != null)
//{
//    var range = JsonConvert.DeserializeObject<List<int>>(randomizePermanentRotationDirectionRequest.ToString());
//    components.Add(new CRandomizePermanentRotationDirectionRequest { Range = range });
//}

//if (randomizeRotationSpeedRequest != null)
//{
//    var range = JsonConvert.DeserializeObject<List<float>>(randomizeRotationSpeedRequest.ToString());
//    components.Add(new CRandomizeRotationSpeedRequest { Range = range });
//}


//if (rotationSpeed != null)
//{
//    components.Add(new CRotationSpeed { Value = (float)rotationSpeed });
//}

//if (accelerationModifier != null)
//{
//    components.Add(new CAccelerationModifier { Value = (float)accelerationModifier });
//}

//if (deaccelerationModifier != null)
//{
//    components.Add(new CDeaccelerationModifier { Value = (float)deaccelerationModifier });
//}


//if (maxVelocityMagnitude != null)
//{
//    components.Add(new CMaxVelocityMagnitude { Value = (float)maxVelocityMagnitude });
//}

//if (spawnPrimaryWeaponRequest != null)
//{
//    components.Add(new CSpawnPrimaryWeaponRequest { ConfigAddress = spawnPrimaryWeaponRequest.ToString() });
//}

//if (spawnSecondaryWeaponRequest != null)
//{
//    components.Add(new CSpawnSecondaryWeaponRequest { ConfigAddress = spawnSecondaryWeaponRequest.ToString() });
//}

//////WEAPONS

//if (coolDown != null)
//{
//    components.Add(new CCoolDown { Value = (float)coolDown });
//}

//if (bulletGunTag != null)
//{
//    components.Add(new CBulletGunTag { });
//}

//if (laserGunTag != null)
//{
//    components.Add(new CLaserGunTag { });
//}


//////PROJECTILES
//if (bulletTag != null)
//{
//    components.Add(new CBulletTag { });
//}


//////ON OUT OF LEVEL
//if (onOutOfLevel != null)
//{
//    components.Add(new COnOutOfLevel
//    {
//        Components = JsonConvert.DeserializeObject<ComponentList>(onOutOfLevel.ToString())
//    });
//}




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