using Zenject;

namespace AsteroidsProject.Infrastructure.Obstacles.Asteroid
{
    public interface IAsteroidPresenterFactory : IFactory<IAsteroidInitParams, IAsteroidPresenter>
    {
    }
}