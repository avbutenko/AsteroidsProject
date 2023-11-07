using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.MonoBehaviours
{
    public abstract class GameObjectView : MonoBehaviour, IGameObjectLink
    {
        public Vector2 Position { set => transform.localPosition = value; }
        public Quaternion Rotation { set => transform.localRotation = value; }

        public int GetGameObjectInstanceID()
        {
            return transform.gameObject.GetInstanceID();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}