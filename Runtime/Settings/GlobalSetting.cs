// *********************************************************************************
// # Project: JFramework
// # Unity: 2022.3.5f1c1
// # Author: Charlotte
// # Version: 1.0.0
// # History: 2023-10-24  23:55
// # Copyright: 2023, Charlotte
// # Description: This is an automatically generated comment.
// *********************************************************************************

using Sirenix.OdinInspector;
using UnityEngine;

namespace JFramework
{
    internal class GlobalSetting : AssetSingleton<GlobalSetting>
    {
        /// <summary>
        /// 构建平台
        /// </summary>
        public AssetPlatform platform =
#if UNITY_STANDALONE_WIN
            AssetPlatform.StandaloneWindows;
#elif UNITY_STANDALONE_OSX
            AssetPlatform.StandaloneOSX;
#elif UNITY_ANDROID
            AssetPlatform.Android;
#elif UNITY_IPHONE
            AssetPlatform.IOS;
#endif

        /// <summary>
        /// AssetBundle 校验文件名称
        /// </summary>
        public string assetInfo = "AssetBundleInfo";

        /// <summary>
        /// 从远端加载 AssetBundle 的路径
        /// </summary>
        public string remotePath = "http://192.168.0.3:8000/AssetBundles";

        /// <summary>
        /// 客户端校验文件名称
        /// </summary>
        [ShowInInspector]
        public static string clientInfoName => $"{Instance.assetInfo}.json";

        /// <summary>
        /// 客户端校验文件名称
        /// </summary>
        [ShowInInspector]
        private static string remoteInfoName => $"{Instance.assetInfo}_TMP.json";

        /// <summary>
        /// 客户端校对文件路径
        /// </summary>
        [ShowInInspector]
        public static string clientInfoPath => $"{Application.persistentDataPath}/{clientInfoName}";

        /// <summary>
        /// 服务器校对文件路径
        /// </summary>
        [ShowInInspector]
        public static string remoteInfoPath => $"{Application.persistentDataPath}/{remoteInfoName}";

        /// <summary>
        /// 本地校验文件路径
        /// </summary>
        [ShowInInspector]
        public static string streamingInfoPath =>
#if UNITY_ANDROID
            $"file://{Application.streamingAssetsPath}/{Instance.platform}/{clientInfoName}";
#else
            $"{Application.streamingAssetsPath}/{Instance.platform}/{clientInfoName}";
#endif

        /// <summary>
        /// 根据 persistentDataPath 获取文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetPersistentPath(string fileName) => $"{Application.persistentDataPath}/{fileName}";

        /// <summary>
        /// 根据 streamingAssetsPath 获取文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetStreamingPath(string fileName) => $"{Application.streamingAssetsPath}/{Instance.platform}/{fileName}";

        /// <summary>
        /// 获取远端文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetRemoteFilePath(string fileName) => $"{Instance.remotePath}/{Instance.platform}/{fileName}";
    }
}