using UnityEngine;

namespace AsteroidsProject.Infrastructure.Services
{
    public interface IInputService
    {
        public bool IsAccelerating { get; }
        public bool IsDeaccelerating { get; }
        public bool IsRotating { get; }
        public float RotationDirection { get; }
    }
}