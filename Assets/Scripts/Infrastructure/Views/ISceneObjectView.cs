using UnityEngine;

namespace AsteroidsProject.Infrastructure.Views
{
    public interface ISceneObjectView
    {
        public void UpdatePosition(Vector3 position);
        public void UpdateRotation(Quaternion rotation);
        public void UpdateScale(float scale);
        public void DestroyView();
    }
}