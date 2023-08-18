using System;

namespace AsteroidsProject.Infrastructure
{
    public interface IMovableWithOnChangeEvents : IMovable
    {
        public event Action<float> MovementSpeedChanged;
    }
}