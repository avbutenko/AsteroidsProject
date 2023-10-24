﻿using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Core
{
    public class RotationSpeedConverter : IJTokenConverter
    {
        public string TokenName => nameof(CRotationSpeed);

        public object Convert(JToken token)
        {
            return new CRotationSpeed { Value = (float)token };
        }
    }
}