namespace AsteroidsProject.Shared
{
    public interface IActiveGameObjectMapService
    {
        public void Add(IGameObject key, GoEntityPair valuePair);
        public void Remove(IGameObject key);
        public bool Has(IGameObject key);
        public GoEntityPair GetValuePair(IGameObject key);
    }
}