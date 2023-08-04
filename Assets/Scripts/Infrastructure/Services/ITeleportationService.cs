using AsteroidsProject.Infrastructure.Services;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services
{
    public interface ITeleportationService
    {
        public bool IsOutOfLevel(Vector3 currentPosition, float scale);
        public Vector3 Teleport(Vector3 currentPosition, float scale);
    }
}