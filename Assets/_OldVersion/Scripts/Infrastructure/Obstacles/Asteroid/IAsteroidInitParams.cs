using UnityEngine;

namespace AsteroidsProject.Infrastructure.Obstacles.Asteroid
{
    public interface IAsteroidInitParams : ITransformable, IMovable, IRotatable
    {
        public Sprite Sprite { get; }
    }
}