using UnityEngine;

namespace Game.Scripts.Main.Runtime.GameEnum
{
    public enum InitGameType
    {
        [InspectorName("开始")] Begin = 0,

        [InspectorName("地图")] Map = 0,
        [InspectorName("宗门")] Sect = 1,
        [InspectorName("家族")] Family = 2,
        [InspectorName("Npc")] Npc = 3,
        [InspectorName("功法")] MartialArts = 4,

        [InspectorName("结束")] End = 5,
    }
}