using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.GameLogic.Features.AccelerationMovement;
using AsteroidsProject.GameLogic.Features.DeaccelerationMovement;
using AsteroidsProject.GameLogic.Features.MaxVelocityMagnitude;
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
                JToken randomizePermanentRotationDirectionRequest = e["CRandomizePermanentRotationDirectionRequest"];
                JToken randomizeRotationSpeedRequest = e["CRandomizeRotationSpeedRequest"];

                JToken playerTag = e["CPlayerTag"];
                JToken rotationSpeed = e["CRotationSpeed"];
                JToken accelerationModifier = e["CAccelerationModifier"];
                JToken deaccelerationModifier = e["CDeaccelerationModifier"];
                JToken velocity = e["CVelocity"];
                JToken maxVelocityMagnitude = e["CMaxVelocityMagnitude"];
                JToken spawnPrimaryWeaponRequest = e["CSpawnPrimaryWeaponRequest"];
                JToken spawnSecondaryWeaponRequest = e["CSpawnSecondaryWeaponRequest"];
                JToken coolDown = e["CCoolDown"];
                JToken bulletGunTag = e["CBulletGunTag"];
                JToken laserGunTag = e["CLaserGunTag"];
                JToken bulletTag = e["CBulletTag"];



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

                //if (rotation != null)
                //{
                //    components.Add(new CRotation { Value = new Quaternion(0, 0, (float)rotation, 1) });
                //}

                if (randomizePositionRequest != null)
                {
                    components.Add(new CRandomizePositionRequest());
                }

                if (randomizeVelocityRequest != null)
                {
                    var range = JsonConvert.DeserializeObject<List<Vector2>>(randomizeVelocityRequest.ToString());
                    components.Add(new CRandomizeVelocityRequest { Range = range });
                }

                if (randomizePermanentRotationDirectionRequest != null)
                {
                    var range = JsonConvert.DeserializeObject<List<int>>(randomizePermanentRotationDirectionRequest.ToString());
                    components.Add(new CRandomizePermanentRotationDirectionRequest { Range = range });
                }

                if (randomizeRotationSpeedRequest != null)
                {
                    var range = JsonConvert.DeserializeObject<List<float>>(randomizeRotationSpeedRequest.ToString());
                    components.Add(new CRandomizeRotationSpeedRequest { Range = range });
                }



                ////PLAYER
                if (playerTag != null)
                {
                    components.Add(new CPlayerTag());
                }

                if (rotationSpeed != null)
                {
                    components.Add(new CRotationSpeed { Value = (float)rotationSpeed });
                }

                if (accelerationModifier != null)
                {
                    components.Add(new CAccelerationModifier { Value = (float)accelerationModifier });
                }

                if (deaccelerationModifier != null)
                {
                    components.Add(new CDeaccelerationModifier { Value = (float)deaccelerationModifier });
                }

                if (velocity != null)
                {
                    var value = JsonConvert.DeserializeObject<Vector2>(velocity.ToString());
                    components.Add(new CVelocity { Value = value });
                }

                if (maxVelocityMagnitude != null)
                {
                    components.Add(new CMaxVelocityMagnitude { Value = (float)maxVelocityMagnitude });
                }

                if (spawnPrimaryWeaponRequest != null)
                {
                    components.Add(new CSpawnPrimaryWeaponRequest { ConfigAddress = spawnPrimaryWeaponRequest.ToString() });
                }

                if (spawnSecondaryWeaponRequest != null)
                {
                    components.Add(new CSpawnSecondaryWeaponRequest { ConfigAddress = spawnSecondaryWeaponRequest.ToString() });
                }

                ////WEAPONS

                if (coolDown != null)
                {
                    components.Add(new CCoolDown { Value = (float)coolDown });
                }

                if (bulletGunTag != null)
                {
                    components.Add(new CBulletGunTag { });
                }

                if (laserGunTag != null)
                {
                    components.Add(new CLaserGunTag { });
                }


                ////PROJECTILES
                if (bulletTag != null)
                {
                    components.Add(new CBulletTag { });
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