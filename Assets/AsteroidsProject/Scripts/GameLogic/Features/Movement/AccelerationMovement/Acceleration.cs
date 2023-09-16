using System;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.AccelerationMovement
{
    [Serializable]
    public struct Acceleration
    {
        public Vector2 Vector;
        public float AccelerationModifier;
        public float DeaccelerationModifier;
    }
}