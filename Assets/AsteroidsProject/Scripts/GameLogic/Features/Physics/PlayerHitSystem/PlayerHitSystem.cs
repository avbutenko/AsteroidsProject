using AsteroidsProject.Shared;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.PlayerHitSystem
{
    public class PlayerHitSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<OnTriggerEnter2DEvent>().End();

            var eventPool = world.GetPool<OnTriggerEnter2DEvent>();

            foreach (var entity in filter)
            {
                ref var senderGameObject = ref eventPool.Get(entity).senderGameObject;
                ref var collider = ref eventPool.Get(entity).collider2D;

                var isPlayer = senderGameObject.TryGetComponent(out IPlayerView playerView);
                var isEnemy = collider.gameObject.TryGetComponent(out IEnemyView enemyView);

                if (isPlayer && isEnemy)
                {
                    Debug.Log("Game Over!");
                    eventPool.Del(entity);
                }
            }
        }
    }
}
