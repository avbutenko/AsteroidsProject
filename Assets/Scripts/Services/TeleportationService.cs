using Assets.Scripts.Infrastructure.Services;
using AsteroidsProject.Infrastructure.Services;
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

        public bool IsOutOfLevel(Vector3 currentPosition, float scale)
        {
            if ((currentPosition.x > level.Right + scale && IsMovingInDirection(currentPosition, Vector3.right))
                || (currentPosition.x < level.Left - scale && IsMovingInDirection(currentPosition, -Vector3.right))
                || (currentPosition.y < level.Bottom - scale && IsMovingInDirection(currentPosition, -Vector3.up))
                || (currentPosition.y > level.Top + scale && IsMovingInDirection(currentPosition, Vector3.up)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Vector3 Teleport(Vector3 currentPosition, float scale)
        {
            LevelSide teleportingDirection = GetTeleportingDirection(currentPosition, scale);
            Vector3 newPosition = GetTeleportingPosition(currentPosition, scale, teleportingDirection);
            return newPosition;
        }

        private bool IsMovingInDirection(Vector3 moveDir, Vector3 checkDir)
        {
            return Vector3.Dot(checkDir, moveDir) > 0;
        }

        private LevelSide GetTeleportingDirection(Vector3 currentPosition, float scale)
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

        private Vector3 GetTeleportingPosition(Vector3 currentPosition, float scale, LevelSide direction)
        {
            switch (direction)
            {
                case LevelSide.Top:
                    {
                        return new Vector3(currentPosition.x, (level.Top + scale), currentPosition.z);
                    }
                case LevelSide.Bottom:
                    {
                        return new Vector3(currentPosition.x, (level.Bottom - scale), currentPosition.z);
                    }
                case LevelSide.Right:
                    {
                        return new Vector3((level.Right + scale), currentPosition.y, currentPosition.z);
                    }
                case LevelSide.Left:
                    {
                        return new Vector3((level.Left - scale), currentPosition.y, currentPosition.z);
                    }
                default:
                    {
                        return currentPosition;
                    }
            }
        }
    }
}