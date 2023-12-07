using UniRx;

namespace AsteroidsProject.Shared
{
    public interface IHaveRotationRx
    {
        public IReactiveProperty<float> Rotation { get; }
    }
}