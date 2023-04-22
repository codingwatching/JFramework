using System;
using System.Collections.Generic;
using JFramework.Interface;
using UnityEngine;

namespace JFramework.Core
{
    /// <summary>
    /// 命令管理器
    /// </summary>
    public static class CommandManager
    {
        /// <summary>
        /// 命令存储字典
        /// </summary>
        internal static Dictionary<Type, ICommand> commandDict;

        /// <summary>
        /// 管理器名称
        /// </summary>
        private static string Name => nameof(CommandManager);

        /// <summary>
        /// 命令管理器初始化
        /// </summary>
        internal static void Awake() => commandDict = new Dictionary<Type, ICommand>();

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="value">传入的参数</param>
        /// <typeparam name="T">传入继承ICommand的对象</typeparam>
        public static void Execute<T>(params object[] value) where T : struct, ICommand
        {
            if (commandDict == null)
            {
                Debug.Log($"{Name.Red()} 没有初始化!");
                return;
            }

            var key = typeof(T);
            if (!commandDict.ContainsKey(key))
            {
                var command = new T();
                commandDict.Add(key, command);
                command.OnExecute(value);
                return;
            }

            commandDict[key].OnExecute(value);
        }

        /// <summary>
        /// 移除命令
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Dispose<T>() where T : struct, ICommand
        {
            if (commandDict == null)
            {
                Debug.Log($"{Name.Red()} 没有初始化!");
                return;
            }

            var key = typeof(T);
            if (commandDict.ContainsKey(key))
            {
                commandDict.Remove(key);
            }
        }

        /// <summary>
        /// 清除命令管理器
        /// </summary>
        internal static void Destroy() => commandDict = null;
    }
}