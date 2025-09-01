using UnityEngine;

namespace Game.Scripts.Main.Runtime.Procedure.Scene
{
    public enum SceneType
    {
        [InspectorName("菜单")] Menu = 1,
        [InspectorName("主场景（测试）")] Main = 2,
        [InspectorName("主场景")] Home = 3,
        [InspectorName("战斗")] Battle = 4
    }
}