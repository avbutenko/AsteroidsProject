using System;
using UnityEngine;

namespace AsteroidsProject.Infrastructure
{
    public interface ITransformableWithOnChangeEvents : ITransformable
    {
        public event Action<Vector3> PositionChanged;
        public event Action<Quaternion> RotationChanged;
        public event Action<float> ScaleChanged;
    }
}