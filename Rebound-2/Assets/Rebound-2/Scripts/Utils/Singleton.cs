using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound2.Utils
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        [Header("Singleton Settings")]
        public bool IsPersistent = true;

        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        Debug.Log("No instance found. Is one in the scene?", instance);
                    }
                }

                return instance;
            }
        }
    }
}