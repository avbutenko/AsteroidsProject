using UnityEngine;

namespace AsteroidsProject.Infrastructure.Services
{
    public interface IRotationService
    {
        public Quaternion GetNewRotation(Quaternion currentRotation, float rotationDirection, float rotationSpeed);
    }
}