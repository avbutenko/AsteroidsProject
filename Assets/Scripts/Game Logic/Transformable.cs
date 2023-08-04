using AsteroidsProject.Infrastructure;
using System;
using UnityEngine;

namespace AsteroidsProject.GameLogic
{
    public class Transformable : ITransformable
    {
        public event Action<Vector3> PositionChanged;
        public event Action<Quaternion> RotationChanged;
        public event Action<float> ScaleChanged;

        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public float Scale { get; set; }
    }
}