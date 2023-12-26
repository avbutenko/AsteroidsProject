using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Ammo
{
    public class ClearChangeAmmoAmountRequestSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CChangeAmmoAmountRequest> requestPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CChangeAmmoAmountRequest>().End();
            requestPool = world.GetPool<CChangeAmmoAmountRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                requestPool.Del(entity);
            }
        }
    }
}