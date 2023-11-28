using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IEntityGameObjectFactory
    {
        public UniTask<GameObject> CreateAsync(SpawnEntityViewInfo spawnInfo);
    }
}