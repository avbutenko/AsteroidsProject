using System;

namespace AsteroidsProject.Infrastructure.Services
{
    public interface ISceneLoader
    {
        void Load(string name, Action onLoaded = null);
    }
}