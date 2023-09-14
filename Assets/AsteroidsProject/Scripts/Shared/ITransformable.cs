using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface ITransformable
    {
        public Vector2 Position { set; }
        public Quaternion Rotation { set; }
        public float Scale { set; }
    }
}