using AsteroidsProject.Shared;
using System;

namespace AsteroidsProject.GameLogic.Core
{
    [Serializable]
    public struct LinkToPlayerGameObject
    {
        public IPlayerGameObject View;
    }
}