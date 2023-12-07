using UniRx;

namespace AsteroidsProject.Shared
{
    public interface IHaveHealthRx
    {
        public IReactiveProperty<int> Health { get; }
    }
}