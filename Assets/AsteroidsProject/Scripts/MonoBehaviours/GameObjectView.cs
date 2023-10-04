using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.MonoBehaviours
{
    public abstract class GameObjectView : MonoBehaviour, ILinkToGameObject
    {
        private EcsPackedEntity entity;
        public EcsPackedEntity Entity
        {
            get => entity;
            set => entity = value;
        }

        public Vector2 Position { set => transform.localPosition = value; }
        public Quaternion Rotation { set => transform.localRotation = value; }
        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}