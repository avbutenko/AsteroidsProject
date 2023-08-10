using UnityEngine;

namespace AsteroidsProject.Infrastructure
{
    public interface IMovable
    {
        public float MovementSpeed { get; set; }
        public Vector3 MovementDirection { get; set; }
    }
}