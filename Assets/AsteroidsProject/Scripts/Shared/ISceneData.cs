using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface ISceneData
    {
        public Transform SpawnPlayerPoint { get; }
        public Transform AsteroidsPool { get; }
        public Transform ProjectilePool { get; }
    }
}