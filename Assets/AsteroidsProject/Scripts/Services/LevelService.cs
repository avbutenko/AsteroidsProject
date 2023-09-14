using AsteroidsProject.Shared;
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

        public bool IsOut(Vector2 currentPosition, float scale)
        {
            if ((currentPosition.x > Right + scale && IsMovingInDirection(currentPosition, Vector2.right))
                || (currentPosition.x < Left - scale && IsMovingInDirection(currentPosition, -Vector2.right))
                || (currentPosition.y < Bottom - scale && IsMovingInDirection(currentPosition, -Vector2.up))
                || (currentPosition.y > Top + scale && IsMovingInDirection(currentPosition, Vector2.up)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Vector2 GetOppositePosition(Vector2 currentPosition, float scale)
        {
            var oppositeSide = GetOppositeSide(currentPosition, scale);

            switch (oppositeSide)
            {
                case LevelSide.Top:
                    {
                        return new Vector2(currentPosition.x, (Top + scale));
                    }
                case LevelSide.Bottom:
                    {
                        return new Vector2(currentPosition.x, (Bottom - scale));
                    }
                case LevelSide.Right:
                    {
                        return new Vector2((Right + scale), currentPosition.y);
                    }
                case LevelSide.Left:
                    {
                        return new Vector2((Left - scale), currentPosition.y);
                    }
                default:
                    {
                        return currentPosition;
                    }
            }
        }

        private bool IsMovingInDirection(Vector2 moveDir, Vector2 checkDir)
        {
            return Vector2.Dot(checkDir, moveDir) > 0;
        }

        private LevelSide GetOppositeSide(Vector2 currentPosition, float scale)
        {
            if (currentPosition.x > Right + scale)
            {
                return LevelSide.Left;
            }
            else if (currentPosition.x < Left - scale)
            {
                return LevelSide.Right;
            }
            else if (currentPosition.y < Bottom - scale)
            {
                return LevelSide.Top;
            }
            else if (currentPosition.y > Top + scale)
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