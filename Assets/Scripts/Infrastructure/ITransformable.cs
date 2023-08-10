using UnityEngine;

namespace AsteroidsProject.Infrastructure
{
    public interface ITransformable
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public float Scale { get; set; }
    }
}