using AsteroidsProject.Infrastructure.Obstacles.Asteroid;
using UnityEngine;

namespace AsteroidsProject.Data.Obstacles.Asteroid
{
    public class AsteroidInitParams : IAsteroidInitParams
    {
        public Sprite Sprite { get; set; }
        public int RotationDirection { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public float Scale { get; set; }
        public float MovementSpeed { get; set; }
        public Vector3 MovementDirection { get; set; }
        public float RatationSpeed { get; set; }

    }
}