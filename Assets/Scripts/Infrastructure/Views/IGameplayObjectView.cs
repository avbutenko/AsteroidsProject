using UnityEngine;

namespace AsteroidsProject.Infrastructure.Views
{
    public interface IGameplayObjectView
    {
        public Transform Transform { get; }
    }
}