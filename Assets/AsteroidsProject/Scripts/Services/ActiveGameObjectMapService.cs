using AsteroidsProject.Shared;
using System.Collections.Generic;

namespace AsteroidsProject.Services
{
    public class ActiveGameObjectMapService : IActiveGameObjectMapService
    {
        private readonly Dictionary<int, GoEntityPair> cachedObjects;

        public ActiveGameObjectMapService()
        {
            cachedObjects = new Dictionary<int, GoEntityPair>();
        }

        public void Add(IGameObject key, GoEntityPair valuePair)
        {
            cachedObjects.Add(key.GetGameObjectInstanceID(), valuePair);
        }

        public bool Has(IGameObject key)
        {
            return cachedObjects.ContainsKey(key.GetGameObjectInstanceID());
        }

        public GoEntityPair GetValuePair(IGameObject key)
        {
            return cachedObjects[key.GetGameObjectInstanceID()];
        }

        public void Remove(IGameObject gameObject)
        {
            cachedObjects.Remove(gameObject.GetGameObjectInstanceID());
        }
    }
}