using AsteroidsProject.Shared;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class GameObjectPool : IGameObjectPool, IDisposable, IRestartable
    {
        private readonly Dictionary<int, Stack<GameObject>> cachedObjects;
        private readonly Dictionary<int, int> cachedIDs;

        public GameObjectPool()
        {
            cachedObjects = new Dictionary<int, Stack<GameObject>>();
            cachedIDs = new Dictionary<int, int>();
        }

        public void Register(GameObject prefab, GameObject instance)
        {
            var prefabID = prefab.GetInstanceID();
            var instanceID = instance.GetInstanceID();

            if (!cachedObjects.TryGetValue(prefabID, out _))
            {
                cachedObjects.Add(prefabID, new Stack<GameObject>());
            }

            cachedIDs.Add(instanceID, prefabID);
        }

        public bool HasObjects(GameObject prefab)
        {
            var prefabID = prefab.GetInstanceID();
            return cachedObjects.TryGetValue(prefabID, out var objects) && objects.Count > 0;
        }

        public void Push(GameObject instance)
        {
            var instanceID = instance.GetInstanceID();
            var prefabID = cachedIDs[instanceID];
            instance.SetActive(false);
            cachedObjects[prefabID].Push(instance);
        }

        public GameObject Pull(GameObject prefab)
        {
            var prefabID = prefab.GetInstanceID();
            cachedObjects.TryGetValue(prefabID, out var objects);
            var instance = objects.Pop();
            instance.SetActive(true);
            return instance;
        }

        public void Restart()
        {
            Dispose();
        }

        public void Dispose()
        {
            cachedObjects.Clear();
            cachedIDs.Clear();
        }
    }
}