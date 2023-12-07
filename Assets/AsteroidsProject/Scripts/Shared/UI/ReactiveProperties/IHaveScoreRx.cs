using UniRx;

namespace AsteroidsProject.Shared
{
    public interface IHaveScoreRx
    {
        public IReactiveProperty<int> Score { get; }
    }
}