using Zenject;

namespace AsteroidsProject.Infrastructure.StateMachine
{
    public interface ITickableStateMachine : ITickable
    {
        public void Enter<TState>() where TState : class, ITickableState;
    }
}