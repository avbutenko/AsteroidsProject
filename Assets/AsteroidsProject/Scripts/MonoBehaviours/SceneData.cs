using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.MonoBehaviours
{
    public class SceneData : MonoBehaviour, ISceneData
    {
        [SerializeField] private Transform spawnPlayerPosition;

        public Transform SpawnPlayerPosition => spawnPlayerPosition;
    }
}