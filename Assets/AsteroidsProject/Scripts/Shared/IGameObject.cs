using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IGameObject : ITransformable
    {
        public GameObject GameObject { get; }
    }
}