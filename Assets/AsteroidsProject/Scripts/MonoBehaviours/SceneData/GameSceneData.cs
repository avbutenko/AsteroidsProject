using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.MonoBehaviours
{
    public class GameSceneData : MonoBehaviour, IGameSceneData
    {
        [SerializeField] private Transform spawnPlayerPoint;

        public Transform SpawnPlayerPoint => spawnPlayerPoint;
    }
}