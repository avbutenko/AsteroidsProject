using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface ILinkToGameObject
    {
        public EcsPackedEntity Entity { get; set; }
        public Vector2 Position { set; }
        public Quaternion Rotation { set; }
        public void DestroySelf();
    }
}