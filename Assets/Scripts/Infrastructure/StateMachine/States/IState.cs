namespace AsteroidsProject.Infrastructure.StateMachine
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}