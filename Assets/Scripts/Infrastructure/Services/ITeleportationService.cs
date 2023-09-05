using UnityEngine;

namespace AsteroidsProject.Infrastructure.Services
{
    public interface ITeleportationService
    {
        public bool IsOutOfLevel(Vector2 currentPosition, float scale);
        public Vector2 Teleport(Vector2 currentPosition, float scale);
    }
}