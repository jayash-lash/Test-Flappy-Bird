using System;
using System.Collections.Generic;
using IoC;
using UnityEngine;

namespace Service
{
    public class ObjectPools : MonoBehaviour, IService
    {
        [Serializable]
        public class Pool
        {
            public string Tag;
            public GameObject Prefab;
            public int Size;
        }
        public Type ServiceType => typeof(ObjectPools); 
    
        public List<Pool> Pools;
        public Dictionary<string, Queue<GameObject>> PoolDictionary = new Dictionary<string, Queue<GameObject>>();
    
        public void Awake()
        {
            foreach (var pool in Pools)
            {
                var objectsPool = new Queue<GameObject>();
            
                for (var i = 0; i < pool.Size; i++)
                {
                    GameObject item = Instantiate(pool.Prefab);
                    item.SetActive(false);
                    objectsPool.Enqueue(item);
                }
            
                PoolDictionary.Add(pool.Tag, objectsPool);
            }
        }

        public GameObject SpawnFromPool(string tagName, Vector3 position, Quaternion rotation)
        {
            if (!PoolDictionary.ContainsKey(tagName))
            {
                Debug.LogWarning("Pool with tag" + tagName + "doesn't exist!");
                return null;
            }
        
            var objectToSpawn = PoolDictionary[tagName].Dequeue();
        
            objectToSpawn.SetActive(true);
       
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
        
            var pooledObject = objectToSpawn.GetComponent<IPooledObject>();

            if (pooledObject != null)
            {
                pooledObject.OnObjectSpawned();
            }
        
            PoolDictionary[tagName].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
    }

    public interface IPooledObject
    {
        void OnObjectSpawned();
    }
}