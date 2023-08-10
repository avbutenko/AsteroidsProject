using System;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.Data.Obstacles.Asteroid
{
    [CreateAssetMenu(menuName = "Resources/Data/Obstacles/Asteroid")]
    public class AsteroidSettingsInstaller : ScriptableObjectInstaller<AsteroidSettingsInstaller>
    {
        public AsteroidPrefabSettings AsteroidPrefab;
        public AsteroidGeneralSettings AsteroidParams;

        [Serializable]
        public class AsteroidPrefabSettings
        {
            [field: SerializeField] public GameObject Prefab { get; private set; }
        }

        [Serializable]
        public class AsteroidGeneralSettings
        {
            [field: SerializeField] public int StartingSpawns { get; private set; }
            [field: SerializeField] public int MaxSpawns { get; private set; }
            [field: SerializeField] public float MaxSpawnTime { get; private set; }
            [field: SerializeField] public Sprite[] Sprites { get; private set; }
            [field: SerializeField] public float MinScale { get; private set; }
            [field: SerializeField] public float MaxScale { get; private set; }
            [field: SerializeField] public float MinMovementSpeed { get; private set; }
            [field: SerializeField] public float MaxMovementSpeed { get; private set; }
            [field: SerializeField] public float MinRotationSpeed { get; private set; }
            [field: SerializeField] public float MaxRotationSpeed { get; private set; }
        }

        public override void InstallBindings()
        {
            Container.BindInstance(AsteroidPrefab).IfNotBound();
            Container.BindInstance(AsteroidParams).IfNotBound();
        }
    }
}

