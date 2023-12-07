using UniRx;

namespace AsteroidsProject.Shared
{
    public interface IGameOverScreenModel
    {
        public IReactiveProperty<int> Score { get; }
    }
}