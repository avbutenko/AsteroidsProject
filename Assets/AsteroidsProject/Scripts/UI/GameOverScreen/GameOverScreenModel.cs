using AsteroidsProject.Shared;
using UniRx;

namespace AsteroidsProject.UI.GameOverScreen
{
    public class GameOverScreenModel : IGameOverScreenModel
    {
        public IReactiveProperty<int> Score { get; private set; }

        public GameOverScreenModel()
        {
            Score = new ReactiveProperty<int>(0);
        }
    }
}