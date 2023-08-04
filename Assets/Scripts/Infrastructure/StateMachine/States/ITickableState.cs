using Zenject;

namespace AsteroidsProject.Infrastructure.StateMachine
{
    public interface ITickableState : IExitableState, ITickable
    {
        void Enter();
    }
}