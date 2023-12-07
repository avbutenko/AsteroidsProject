using UniRx;

namespace AsteroidsProject.Shared
{
    public interface IHaveVelocityRx
    {
        public IReactiveProperty<float> Velocity { get; }
    }
}