// *********************************************************************************
// # Project: Test
// # Unity: 2022.3.5f1c1
// # Author: Charlotte
// # Version: 1.0.0
// # History: 2024-02-04  13:29
// # Copyright: 2024, Charlotte
// # Description: This is an automatically generated comment.
// *********************************************************************************

namespace JFramework
{
    public enum UILayer : byte
    {
        Bottom = 0,
        Normal = 1,
        Middle = 2,
        Height = 3,
        Ignore = 4
    }

    public enum UIState : byte
    {
        Common = 0,
        Freeze = 1,
        DontDestroy = 2
    }

    public enum InputMode : byte
    {
        Up = 0,
        Press = 1,
        Down = 2,
        AxisX = 3,
        AxisY = 4,
        AxisRawX = 5,
        AxisRawY = 6,
    }

    public enum AssetMode : byte
    {
        Simulate,
        AssetBundle
    }

    public enum BuildMode : byte
    {
        StreamingAssets,
        BuildPath,
    }

    public enum AssetPlatform : byte
    {
        StandaloneOSX = 2,
        StandaloneWindows = 5,
        IOS = 9,
        Android = 13,
        WebGL = 20
    }
}