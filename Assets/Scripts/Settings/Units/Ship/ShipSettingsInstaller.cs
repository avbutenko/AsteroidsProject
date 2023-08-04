using System;
using UnityEngine;
using Zenject;


namespace AsteroidsProject.Settings.Units.Ship
{
    [CreateAssetMenu(menuName = "Resources/Settings/Units/Ship")]
    public class ShipSettingsInstaller : ScriptableObjectInstaller<ShipSettingsInstaller>
    {
        public ShipGeneralSettings GeneralSettings;
        public ShipMovementSettings MovementSettings;

        [Serializable]
        public class ShipGeneralSettings
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
            Container.BindInstance(GeneralSettings).IfNotBound();
            Container.BindInstance(MovementSettings).IfNotBound();
        }
    }
}