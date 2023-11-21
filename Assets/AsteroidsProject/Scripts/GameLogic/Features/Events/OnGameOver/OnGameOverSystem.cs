using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Events.OnGameOver
{
    public class OnGameOverSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CGameOverEvent>().End();
            var eventPool = world.GetPool<CGameOverEvent>();

            foreach (var entity in filter)
            {
                Debug.Log("Gameover"); // show GameOver Window instead
                eventPool.Del(entity);
            }
        }
    }
}