using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface ILevelService
    {
        public bool IsOut(Vector2 currentPosition, float scale);
        public Vector2 GetOppositePosition(Vector2 currentPosition, float scale);
    }
}