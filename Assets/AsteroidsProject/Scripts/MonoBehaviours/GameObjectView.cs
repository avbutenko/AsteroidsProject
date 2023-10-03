using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.MonoBehaviours
{
    public class GameObjectView : MonoBehaviour, IGameObject
    {
        public Transform Transform => transform;

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}