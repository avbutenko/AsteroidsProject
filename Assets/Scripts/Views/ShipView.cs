using AsteroidsProject.Infrastructure.Views;
using UnityEngine;

namespace AsteroidsProject.Views
{
    public class ShipView : MonoBehaviour, ISceneObjectView
    {
        public void UpdatePosition(Vector3 position)
        {
            transform.position = position;
        }

        public void UpdateRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }
        public void UpdateScale(float value)
        {
            transform.localScale = new Vector3(value, value, value);
        }
        public void DestroyView()
        {
            Destroy(gameObject);
        }
    }
}
