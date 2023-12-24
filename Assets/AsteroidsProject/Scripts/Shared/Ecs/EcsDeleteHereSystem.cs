using Leopotam.EcsLite;

namespace AsteroidsProject.Shared
{
    public class EcsDeleteHereSystem<TComponent> : IEcsInitSystem, IEcsRunSystem where TComponent : struct
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<TComponent> pool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<TComponent>().End();
            pool = world.GetPool<TComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var index in filter) pool.Del(index);
        }
    }
}