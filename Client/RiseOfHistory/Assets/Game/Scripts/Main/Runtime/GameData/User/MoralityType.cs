using UnityEngine;

namespace Game.Scripts.Main.Runtime.GameData.User
{
    public enum MoralityType
    {
        [InspectorName("仁德")] Benevolence = (0x01 << 3) + 0x07,
        [InspectorName("中道")] Moderation = (0x01 << 4) + 0x07,
        [InspectorName("诡诈")] Craftiness = (0x01 << 5) + 0x07,
    }
}