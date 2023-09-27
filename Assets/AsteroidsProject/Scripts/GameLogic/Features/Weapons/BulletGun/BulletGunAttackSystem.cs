using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.BulletGun
{
    public class BulletGunAttackSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<BulletGunTag>()
                              .Inc<AttackRequest>()
                              .End();

            foreach (var entity in filter)
            {
                Debug.Log("Spawn Bullet!!!");
            }
        }
    }
}