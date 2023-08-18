using System;

namespace AsteroidsProject.Infrastructure.Services
{
    public interface IShipInputService
    {
        public event Action OnAcceleration;
        public event Action OnStopAcceleration;
        public event Action OnRotation;
        public event Action OnStopRotation;
        public float RotationDirection { get; }
    }
}
