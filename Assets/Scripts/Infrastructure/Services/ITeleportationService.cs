using UnityEngine;

namespace AsteroidsProject.Infrastructure.Services
{
    public interface ITeleportationService
    {
        public bool IsOutOfLevel(Vector3 currentPosition, float scale);
        public Vector3 Teleport(Vector3 currentPosition, float scale);
    }
}