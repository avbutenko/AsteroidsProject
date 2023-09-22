﻿using AsteroidsProject.Shared;
using System;

namespace AsteroidsProject.GameLogic.Core
{
    [Serializable]
    public struct LinkToGameObject
    {
        public IGameObject View;
    }
}