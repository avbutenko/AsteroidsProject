using System;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Core
{
    [Serializable]
    public struct CRandomizeVelocityRequest
    {
        public List<Vector2> Range;
    }
}