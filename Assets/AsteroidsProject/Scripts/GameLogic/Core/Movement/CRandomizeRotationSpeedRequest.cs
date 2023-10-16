using System;
using System.Collections.Generic;

namespace AsteroidsProject.GameLogic.Core
{
    [Serializable]
    public struct CRandomizeRotationSpeedRequest
    {
        public List<float> Range;
    }
}