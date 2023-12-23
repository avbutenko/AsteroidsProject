using Cysharp.Threading.Tasks;
using System;

namespace AsteroidsProject.Shared
{
    public interface IConfigLoader : IDisposable
    {
        public UniTask<T> Load<T>(string configAddress) where T : class;
    }
}