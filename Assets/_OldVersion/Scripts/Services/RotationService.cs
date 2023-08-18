using AsteroidsProject.Infrastructure.Services;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class RotationService : IRotationService
    {
        private const float MAX_ROTATION_DEGREE = 360f;
        public Quaternion GetNewRotation(Quaternion currentRotation, float rotationDirection, float rotationSpeed)
        {
            float deltaValue = rotationDirection * Time.deltaTime * rotationSpeed;
            float newValue = Mathf.Repeat(currentRotation.eulerAngles.z + deltaValue, MAX_ROTATION_DEGREE);
            Quaternion quaternion = Quaternion.Euler(0, 0, newValue);
            return quaternion;
        }
    }
}