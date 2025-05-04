using UnityEngine;

namespace Ru1t3rl.Utilities
{
    public abstract class UnitySingleton<T> : MonoBehaviour where T : UnityEngine.Component
    {
        private static T instance;
#if UNITY_2023_1_OR_NEWER
        public static T Instance => instance ??=
 GameObject.FindFirstObjectByType<T>() ?? new GameObject(typeof(T).ToString()).AddComponent<T>();
#else 
        public static T Instance =>
            instance ??= FindObjectOfType<T>() ?? new GameObject(typeof(T).ToString()).AddComponent<T>();
#endif

        protected virtual void Awake()
        {
            if (Instance.GetHashCode() != this.GetHashCode())
            {
                Debug.LogWarning(
                    $"[{this.GetType()}] There are multiple instance of type {this.GetType()} in the scene. Please make sure there is only one!\n" +
                    $"To prevent any errors I have been destroyed, my parent was {gameObject.name}");
                Destroy(this);
            }
        }
    }
}