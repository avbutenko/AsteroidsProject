using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IGameObject
    {
        public Transform Transform { get; }
        public void DestroySelf();
    }
}