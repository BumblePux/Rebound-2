using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound.Utils
{
    public class ObjectPooler : MonoBehaviour
    {
        [Header("Settings")]
        public List<GameObject> PooledObjects;
        public GameObject ObjectToPool;
        public int PoolCount = 1;

        //---------------------------------------------------------------------------
        private void Start()
        {
            PooledObjects = new List<GameObject>();
            for (int i = 0; i < PoolCount; i++)
            {
                GameObject obj = Instantiate(ObjectToPool);
                obj.SetActive(false);
                PooledObjects.Add(obj);
            }
        }

        //--------------------------------------------------
        public GameObject GetPooledObject()
        {
            for (int i = 0; i < PooledObjects.Count; i++)
            {
                if (!PooledObjects[i].activeInHierarchy)
                {
                    return PooledObjects[i];
                }
            }

            return null;
        }
    }
}