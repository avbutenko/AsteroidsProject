using UniRx;

namespace AsteroidsProject.Shared
{
    public interface IHaveScore
    {
        public IReactiveProperty<string> Score { get; }
    }
}