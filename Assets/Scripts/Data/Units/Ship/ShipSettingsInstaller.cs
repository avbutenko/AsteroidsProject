using System;
using UnityEngine;
using Zenject;


namespace AsteroidsProject.Data.Units.Ship
{
    [CreateAssetMenu(menuName = "Resources/Data/Units/Ship")]
    public class ShipSettingsInstaller : ScriptableObjectInstaller<ShipSettingsInstaller>
    {
        public ShipPrefabSettings ShipPrefab;
        public ShipMovementSettings MovementSettings;

        [Serializable]
        public class ShipPrefabSettings
        {
            [field: SerializeField] public GameObject Prefab { get; private set; }
        }

        [Serializable]
        public class ShipMovementSettings
        {
            [field: SerializeField] public float MovementAcceleration { get; private set; }
            [field: SerializeField] public float MaxMovementSpeed { get; private set; }
            [field: SerializeField] public float RotationSpeed { get; private set; }
        }

        public override void InstallBindings()
        {
            Container.BindInstance(ShipPrefab).IfNotBound();
            Container.BindInstance(MovementSettings).IfNotBound();
        }
    }
}