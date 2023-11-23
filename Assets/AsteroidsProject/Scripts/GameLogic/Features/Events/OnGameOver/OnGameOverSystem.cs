using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Events.OnGameOver
{
    public class OnGameOverSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CGameOverEvent> eventPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CGameOverEvent>().End();
            eventPool = world.GetPool<CGameOverEvent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                Debug.Log("Gameover"); // show GameOver Window instead
                eventPool.Del(entity);
            }
        }
    }
}