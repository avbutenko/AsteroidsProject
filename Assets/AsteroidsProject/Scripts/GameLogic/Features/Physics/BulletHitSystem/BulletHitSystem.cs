using AsteroidsProject.Shared;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.BulletHitSystem
{
    public class BulletHitSystem : IEcsRunSystem
    {
        private readonly IGameObjectPool pool;
        private int hittedEntity;

        public BulletHitSystem(IGameObjectPool pool)
        {
            this.pool = pool;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<OnTriggerEnter2DEvent>().End();

            var eventPool = world.GetPool<OnTriggerEnter2DEvent>();

            foreach (var entity in filter)
            {
                ref var senderGameObject = ref eventPool.Get(entity).senderGameObject;
                ref var collider = ref eventPool.Get(entity).collider2D;

                //var isBullet = senderGameObject.TryGetComponent(out IBulletView playerView);
                //var isEnemy = collider.gameObject.TryGetComponent(out IAsteroidView enemyView);

                //if (isBullet && isEnemy)
                //{
                //    collider.gameObject.TryGetComponent(out ILinkToGameObject link);

                //    link.Entity.Unpack(world, out hittedEntity);
                //    world.DelEntity(hittedEntity);
                //    //Object.Destroy(collider.gameObject);
                //    pool.Push(collider.gameObject);
                //    eventPool.Del(entity);
                //}
            }
        }
    }
}
