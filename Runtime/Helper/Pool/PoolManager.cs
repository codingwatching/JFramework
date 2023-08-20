using System.Collections.Generic;
using System.Threading.Tasks;
using JFramework.Interface;
using UnityEngine;

namespace JFramework.Core
{
    public static class PoolManager
    {
        /// <summary>
        /// 对象池组
        /// </summary>
        internal static readonly Dictionary<string, GameObject> parents = new Dictionary<string, GameObject>();

        /// <summary>
        /// 对象池容器
        /// </summary>
        internal static readonly Dictionary<string, IPool> pools = new Dictionary<string, IPool>();

        /// <summary>
        /// 对象池管理器
        /// </summary>
        private static Transform poolManager;

        /// <summary>
        /// 获取 PoolManager 对象
        /// </summary>
        internal static void Register()
        {
            poolManager = GlobalManager.Instance.transform.Find("PoolManager");
        }

        /// <summary>
        /// 弹出对象池
        /// </summary>
        /// <typeparam name="T">任何可以被new的对象</typeparam>
        /// <returns>返回弹出对象</returns>
        public static T Pop<T>() where T : new()
        {
            if (pools.TryGetValue(typeof(T).Name, out var pool) && pool.Count > 0)
            {
                return ((IPool<T>)pool).Pop();
            }

            return new T();
        }

        /// <summary>
        /// 推入对象池
        /// </summary>
        /// <param name="obj">传入对象</param>
        /// <typeparam name="T">任何可以被new的对象</typeparam>
        public static void Push<T>(T obj) where T : new()
        {
            if (pools.TryGetValue(typeof(T).Name, out var pool))
            {
                ((IPool<T>)pool).Push(obj);
                return;
            }

            pools.Add(typeof(T).Name, new Pool<T>(obj));
        }

        /// <summary>
        /// 对象池管理器异步获取对象
        /// </summary>
        /// <param name="path">弹出对象的路径</param>
        public static async Task<T> Pop<T>(string path) where T : Component
        {
            if (!GlobalManager.Runtime) return null;
            if (pools.TryGetValue(path, out var pool) && pool.Count > 0)
            {
                var pop = ((IPool<GameObject>)pool).Pop();
                if (pop != null)
                {
                    pop.SetActive(true);
                    pop.transform.SetParent(null);
                    pop.GetComponent<IPop>()?.OnPop();
                    return pop.GetComponent<T>();
                }
            }

            var obj = await AssetManager.LoadAsync<GameObject>(path);
            obj.name = path;
            Object.DontDestroyOnLoad(obj);
            obj.GetComponent<IPop>()?.OnPop();
            return obj.GetComponent<T>();
        }

        /// <summary>
        /// 对象池管理器推入对象
        /// </summary>
        /// <param name="obj">对象的实例</param>
        public static void Push(GameObject obj)
        {
            if (!GlobalManager.Runtime) return;
            if (obj == null) return;
            if (pools.TryGetValue(obj.name, out var pool))
            {
                ((IPool<GameObject>)pool).Push(obj);
                obj.SetActive(false);
                obj.transform.SetParent(parents[obj.name].transform);
                obj.GetComponent<IPush>()?.OnPush();
                return;
            }

            parents[obj.name] = new GameObject(obj.name + "-Pool");
            parents[obj.name].transform.SetParent(poolManager);
            obj.SetActive(false);
            obj.transform.SetParent(parents[obj.name].transform);
            obj.GetComponent<IPush>()?.OnPush();
            pools.Add(obj.name, new Pool<GameObject>(obj));
        }

        /// <summary>
        /// 管理器卸载
        /// </summary>
        internal static void UnRegister()
        {
            foreach (var pool in pools.Values)
            {
                pool.Clear();
            }

            pools.Clear();
            parents.Clear();
        }
    }
}