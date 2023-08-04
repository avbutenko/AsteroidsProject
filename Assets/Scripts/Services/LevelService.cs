using AsteroidsProject.Infrastructure.Services;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class LevelService : ILevelService
    {
        private readonly Camera camera;

        public LevelService(
            Camera camera)
        {
            this.camera = camera;
        }

        public float Bottom
        {
            get { return -ExtentHeight; }
        }

        public float Top
        {
            get { return ExtentHeight; }
        }

        public float Left
        {
            get { return -ExtentWidth; }
        }

        public float Right
        {
            get { return ExtentWidth; }
        }

        public float ExtentHeight
        {
            get { return camera.orthographicSize; }
        }

        public float Height
        {
            get { return ExtentHeight * 2.0f; }
        }

        public float ExtentWidth
        {
            get { return camera.aspect * camera.orthographicSize; }
        }

        public float Width
        {
            get { return ExtentWidth * 2.0f; }
        }
    }
}