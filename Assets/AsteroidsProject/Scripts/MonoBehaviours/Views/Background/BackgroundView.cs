using UnityEngine;

namespace AsteroidsProject.MonoBehaviours
{
    public class BackgroundView : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Vector2 offset;
        private Renderer spaceRenderer;
        private void Awake()
        {
            spaceRenderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            offset.y += speed * Time.deltaTime;
            spaceRenderer.material.mainTextureOffset = offset;
        }
    }
}