using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.MonoBehaviours
{
    public class SceneData : MonoBehaviour, ISceneData
    {
        [SerializeField] private Transform spawnPlayerPosition;
        [SerializeField] private Transform asteroidsParent;

        public Transform SpawnPlayerPosition => spawnPlayerPosition;
        public Transform AsteroidsParent => asteroidsParent;
    }
}