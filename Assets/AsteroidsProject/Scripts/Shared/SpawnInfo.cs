using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.Shared
{
    public struct SpawnInfo
    {
        public string PrefabAddress;
        public Vector3 Position;
        public Quaternion Rotation;
        public Transform Parent;
        //public EcsWorld World;
        public EcsPackedEntity OwnerEntity;
    }
}