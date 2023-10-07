using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.MonoBehaviours
{
    public class SceneData : MonoBehaviour, ISceneData
    {
        [SerializeField] private Transform spawnPlayerPosition;
        [SerializeField] private Transform asteroidsPool;
        [SerializeField] private Transform bulletsPool;

        public Transform SpawnPlayerPosition => spawnPlayerPosition;
        public Transform AsteroidsPool => asteroidsPool;
        public Transform BulletsPool => bulletsPool;
    }
}