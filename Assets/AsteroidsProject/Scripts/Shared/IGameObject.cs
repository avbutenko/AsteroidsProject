using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IGameObject
    {
        public Vector2 Position { set; }
        public Quaternion Rotation { set; }
    }
}