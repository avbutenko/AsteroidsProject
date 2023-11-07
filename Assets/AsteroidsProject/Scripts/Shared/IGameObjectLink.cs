using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IGameObjectLink
    {
        public Vector2 Position { set; }
        public Quaternion Rotation { set; }
        public int GetGameObjectInstanceID();
        public void Destroy();
    }
}