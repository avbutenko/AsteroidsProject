using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.MonoBehaviours
{
    public class GameSceneData : MonoBehaviour, IGameSceneData
    {
        [SerializeField] private Transform canvasTransform;
        [SerializeField] private Transform spawnPlayerPoint;
        [SerializeField] private Transform asteroidsPool;
        [SerializeField] private Transform projectilePool;
        [SerializeField] private Transform ufoPool;

        public Transform SpawnPlayerPoint => spawnPlayerPoint;
        public Transform AsteroidsPool => asteroidsPool;
        public Transform ProjectilePool => projectilePool;
        public Transform UfoPool => ufoPool;
        public Transform CanvasTransform => canvasTransform;
    }
}