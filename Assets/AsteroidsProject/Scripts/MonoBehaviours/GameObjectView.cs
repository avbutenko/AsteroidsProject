using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.MonoBehaviours
{
    public class GameObjectView : MonoBehaviour, IGameObject
    {
        public Vector2 Position { set => transform.localPosition = value; }
        public Quaternion Rotation { set => transform.localRotation = value; }
    }
}