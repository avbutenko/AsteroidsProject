using Zenject;

namespace AsteroidsProject.Infrastructure.Units.Ship
{
    public interface IShipPresenterFactory : IFactory<ITransformable, IShipPresenter>
    {
    }
}