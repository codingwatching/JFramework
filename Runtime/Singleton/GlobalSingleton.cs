// *********************************************************************************
// # Project: Test
// # Unity: 2022.3.5f1c1
// # Author: Charlotte
// # Version: 1.0.0
// # History: 2024-02-04  22:46
// # Copyright: 2024, Charlotte
// # Description: This is an automatically generated comment.
// *********************************************************************************

using JFramework.Core;
using UnityEngine;

namespace JFramework
{
    public abstract class GlobalSingleton<T> : MonoBehaviour where T : GlobalSingleton<T>
    {
        private static readonly object locked = typeof(T);

        private static T instance;

        public static T Instance
        {
            get
            {
                if (GlobalManager.Instance)
                {
                    if (instance == null)
                    {
                        lock (locked)
                        {
                            instance ??= FindFirstObjectByType<T>();
                            instance ??= new GameObject(typeof(T).Name).AddComponent<T>();
                            instance.Register();
                        }
                    }
                }

                return instance;
            }
        }

        private void Register()
        {
            if (instance == null)
            {
                instance = (T)this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Debug.LogWarning(typeof(T) + "单例重复！");
                Destroy(this);
            }
        }

        protected virtual void Awake() => Register();

        protected virtual void OnDestroy() => instance = null;
    }
}