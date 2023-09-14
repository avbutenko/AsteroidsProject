using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class TeleportationService : ITeleportationService
    {
        private readonly ILevelService level;
        public TeleportationService(ILevelService level)
        {
            this.level = level;
        }

        public bool IsOutOfLevel(Vector2 currentPosition, float scale)
        {
            if ((currentPosition.x > level.Right + scale && IsMovingInDirection(currentPosition, Vector2.right))
                || (currentPosition.x < level.Left - scale && IsMovingInDirection(currentPosition, -Vector2.right))
                || (currentPosition.y < level.Bottom - scale && IsMovingInDirection(currentPosition, -Vector2.up))
                || (currentPosition.y > level.Top + scale && IsMovingInDirection(currentPosition, Vector2.up)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Vector2 Teleport(Vector2 currentPosition, float scale)
        {
            LevelSide teleportingDirection = GetTeleportingDirection(currentPosition, scale);
            Vector2 newPosition = GetTeleportingPosition(currentPosition, scale, teleportingDirection);
            return newPosition;
        }

        private bool IsMovingInDirection(Vector2 moveDir, Vector2 checkDir)
        {
            return Vector2.Dot(checkDir, moveDir) > 0;
        }

        private LevelSide GetTeleportingDirection(Vector2 currentPosition, float scale)
        {
            if (currentPosition.x > level.Right + scale)
            {
                return LevelSide.Left;
            }
            else if (currentPosition.x < level.Left - scale)
            {
                return LevelSide.Right;
            }
            else if (currentPosition.y < level.Bottom - scale)
            {
                return LevelSide.Top;
            }
            else if (currentPosition.y > level.Top + scale)
            {
                return LevelSide.Bottom;
            }

            return default;
        }

        private Vector2 GetTeleportingPosition(Vector2 currentPosition, float scale, LevelSide direction)
        {
            switch (direction)
            {
                case LevelSide.Top:
                    {
                        return new Vector2(currentPosition.x, (level.Top + scale));
                    }
                case LevelSide.Bottom:
                    {
                        return new Vector2(currentPosition.x, (level.Bottom - scale));
                    }
                case LevelSide.Right:
                    {
                        return new Vector2((level.Right + scale), currentPosition.y);
                    }
                case LevelSide.Left:
                    {
                        return new Vector2((level.Left - scale), currentPosition.y);
                    }
                default:
                    {
                        return currentPosition;
                    }
            }
        }
    }
}