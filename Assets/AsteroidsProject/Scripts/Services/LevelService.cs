using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class LevelService : ILevelService
    {
        private const float EDGE_OFFSET = 1f;

        private readonly Camera camera;

        public LevelService(
            Camera camera)
        {
            this.camera = camera;
        }

        public bool IsOut(Vector2 currentPosition)
        {
            if ((currentPosition.x > Right + EDGE_OFFSET && IsMovingInDirection(currentPosition, Vector2.right))
                || (currentPosition.x < Left - EDGE_OFFSET && IsMovingInDirection(currentPosition, -Vector2.right))
                || (currentPosition.y < Bottom - EDGE_OFFSET && IsMovingInDirection(currentPosition, -Vector2.up))
                || (currentPosition.y > Top + EDGE_OFFSET && IsMovingInDirection(currentPosition, Vector2.up)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Vector2 GetOppositePosition(Vector2 currentPosition)
        {
            var oppositeSide = GetOppositeSide(currentPosition);

            switch (oppositeSide)
            {
                case LevelSide.Top:
                    {
                        return new Vector2(currentPosition.x, (Top + EDGE_OFFSET));
                    }
                case LevelSide.Bottom:
                    {
                        return new Vector2(currentPosition.x, (Bottom - EDGE_OFFSET));
                    }
                case LevelSide.Right:
                    {
                        return new Vector2((Right + EDGE_OFFSET), currentPosition.y);
                    }
                case LevelSide.Left:
                    {
                        return new Vector2((Left - EDGE_OFFSET), currentPosition.y);
                    }
                default:
                    {
                        return currentPosition;
                    }
            }
        }

        public Vector2 GetRandomPosition()
        {
            var side = (LevelSide)Random.Range(0, (int)LevelSide.Count);
            var rand = Random.Range(0.0f, 1.0f);

            switch (side)
            {
                case LevelSide.Top:
                    {
                        return new Vector2(Left + rand * Width, Top + EDGE_OFFSET);
                    }
                case LevelSide.Bottom:
                    {
                        return new Vector2(Left + rand * Width, Bottom - EDGE_OFFSET);
                    }
                case LevelSide.Right:
                    {
                        return new Vector2(Right + EDGE_OFFSET, Bottom + rand * Height);
                    }
                case LevelSide.Left:
                    {
                        return new Vector2(Left - EDGE_OFFSET, Bottom + rand * Height);
                    }
            }

            return Vector2.zero;
        }

        private bool IsMovingInDirection(Vector2 moveDir, Vector2 checkDir)
        {
            return Vector2.Dot(checkDir, moveDir) > 0;
        }

        private LevelSide GetOppositeSide(Vector2 currentPosition)
        {
            if (currentPosition.x > Right + EDGE_OFFSET)
            {
                return LevelSide.Left;
            }
            else if (currentPosition.x < Left - EDGE_OFFSET)
            {
                return LevelSide.Right;
            }
            else if (currentPosition.y < Bottom - EDGE_OFFSET)
            {
                return LevelSide.Top;
            }
            else if (currentPosition.y > Top + EDGE_OFFSET)
            {
                return LevelSide.Bottom;
            }

            return default;
        }

        private float Bottom
        {
            get { return -ExtentHeight; }
        }

        private float Top
        {
            get { return ExtentHeight; }
        }

        private float Left
        {
            get { return -ExtentWidth; }
        }

        private float Right
        {
            get { return ExtentWidth; }
        }

        private float ExtentHeight
        {
            get { return camera.orthographicSize; }
        }

        private float Height
        {
            get { return ExtentHeight * 2.0f; }
        }

        private float ExtentWidth
        {
            get { return camera.aspect * camera.orthographicSize; }
        }

        private float Width
        {
            get { return ExtentWidth * 2.0f; }
        }
    }
}