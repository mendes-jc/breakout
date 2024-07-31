using UnityEngine;

namespace Breakout
{
    public abstract class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                string typename = typeof(T).Name;
                Debug.LogWarning($"More than one instance of {typename} found.");
            }
            Instance = this as T;
        }
    }
}