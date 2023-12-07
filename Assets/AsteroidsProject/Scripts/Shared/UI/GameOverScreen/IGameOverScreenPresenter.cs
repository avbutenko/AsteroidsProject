namespace AsteroidsProject.Shared
{
    public interface IGameOverScreenPresenter : IUIScreenPresenter
    {
        public int Score { get; set; }
    }
}