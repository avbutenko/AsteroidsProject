using Leopotam.EcsLite;

namespace AsteroidsProject.Shared
{
    public interface IEcsSystemsFactory
    {
        public IEcsSystem Create(string typeName);
    }
}