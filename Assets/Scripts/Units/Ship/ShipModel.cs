using AsteroidsProject.Infrastructure.Units.Ship;
using System;
using UnityEngine;

namespace AsteroidsProject.Units.Ship
{
    public class ShipModel : IShipModel
    {
        public event Action<Quaternion> RotationChanged;
        public event Action<Vector3> PositionChanged;
        public event Action<float> ScaleChanged;

        private Vector3 position;
        private Quaternion rotation;
        private float scale;
        private float speed;

        public Vector3 Position
        {
            get { return position; }
            set
            {
                position = value;
                PositionChanged?.Invoke(position);
            }
        }

        public Quaternion Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;
                RotationChanged?.Invoke(rotation);
            }
        }

        public float Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                ScaleChanged?.Invoke(scale);
            }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
    }
}
