using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IActiveGOMappingService
    {
        public void Add(int objectInstanceID, GoEntityPair valuePair);
        public void Remove(int objectInstanceID);
        public bool Has(int objectInstanceID);
        public bool TryGetEntity(int objectInstanceID, out EcsPackedEntity? result);
        public bool TryGetGoLink(int objectInstanceID, out IGameObjectLink result);
        public bool TryGetGo(int objectInstanceID, out GameObject result);
    }
}