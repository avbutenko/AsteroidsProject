using System;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Core
{
    [Serializable]
    public struct Acceleration
    {
        public Vector2 Vector;
        public float AccelerationModifier;
        public float DeaccelerationModifier;
    }
}