using UnityEngine;

namespace GameMain.Scripts.Procedure.Scene
{
    public enum SceneType
    {
        [InspectorName("菜单")] Menu = 1,
        [InspectorName("主场景")] Main = 2,
        [InspectorName("游戏")] Game = 3,
        [InspectorName("战斗")] Battle = 4
    }
}