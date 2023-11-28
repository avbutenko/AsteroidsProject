using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.MonoBehaviours
{
    public class MainMenuSceneData : MonoBehaviour, IMainMenuSceneData
    {
        [SerializeField] private Transform canvasTransform;
        public Transform CanvasTransform => canvasTransform;
    }
}