using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class ActiveGOMappingService : IActiveGOMappingService, IDisposable, IRestartable
    {
        private readonly Dictionary<int, GoEntityPair> cachedObjects;

        public ActiveGOMappingService()
        {
            cachedObjects = new Dictionary<int, GoEntityPair>();
        }

        public void Add(int objectInstanceID, GoEntityPair valuePair)
        {
            cachedObjects.Add(objectInstanceID, valuePair);
        }

        public bool Has(int objectInstanceID)
        {
            return cachedObjects.ContainsKey(objectInstanceID);
        }

        public void Remove(int objectInstanceID)
        {
            cachedObjects.Remove(objectInstanceID);
        }

        public bool TryGetGoLink(int objectInstanceID, out IGameObjectLink result)
        {
            if (cachedObjects.TryGetValue(objectInstanceID, out var goEntityPair))
            {
                if (goEntityPair.Go == null)
                {
                    result = null;
                    return false;
                }
                else
                {
                    result = goEntityPair.Go.GetComponent<IGameObjectLink>();
                    return true;
                }
            }
            else
            {
                result = null;
                return false;
            }
        }

        public bool TryGetGo(int objectInstanceID, out GameObject result)
        {
            if (cachedObjects.TryGetValue(objectInstanceID, out var goEntityPair))
            {
                if (goEntityPair.Go == null)
                {
                    result = null;
                    return false;
                }
                else
                {
                    result = goEntityPair.Go;
                    return true;
                }
            }
            else
            {
                result = null;
                return false;
            }
        }

        public bool TryGetEntity(int objectInstanceID, out EcsPackedEntity? result)
        {
            if (cachedObjects.TryGetValue(objectInstanceID, out var goEntityPair))
            {
                result = goEntityPair.PackedEntity;
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public void Restart()
        {
            Dispose();
        }

        public void Dispose()
        {
            foreach (var pair in cachedObjects.Values)
            {
                if (pair.Go != null && pair.Go.TryGetComponent<IGameObjectLink>(out var goLink))
                {
                    goLink.Destroy();
                }
            }

            cachedObjects.Clear();
        }
    }
}