using System;
using UnityEngine;

namespace AsteroidsProject.Infrastructure.Views
{
    public interface ISceneObjectView
    {
        public event Action<Collision2D> OnCollision;
    }
}

