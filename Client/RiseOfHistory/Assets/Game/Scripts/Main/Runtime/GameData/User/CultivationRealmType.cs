using UnityEngine;

namespace Game.Scripts.Main.Runtime.GameData.User
{


    public enum CultivationRealmType
    {
        [InspectorName("炼气")] QiRefining = 1,
        [InspectorName("筑基")] FoundationBuilding = 2,
        [InspectorName("金丹")] GoldenCore = 3,
        [InspectorName("元婴")] NascentSoul = 4,
        [InspectorName("化神")] SoulFormation = 5,
        [InspectorName("炼虚")] VoidRefining = 6,
        [InspectorName("合体")] BodyIntegration = 7,
        [InspectorName("大乘")] Mahayana = 8,
        [InspectorName("渡劫")] Transcendence = 9
    }

}