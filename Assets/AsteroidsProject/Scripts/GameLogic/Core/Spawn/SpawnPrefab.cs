using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Core
{
    public struct SpawnPrefab
    {
        public string PrefabAddress;
        public Vector3 Position;
        public Quaternion Rotation;
        public Transform Parent;
        public EcsPackedEntity OwnerEntity;
    }
}