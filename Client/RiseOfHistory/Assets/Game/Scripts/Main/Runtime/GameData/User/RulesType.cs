using UnityEngine;

namespace Game.Scripts.Main.Runtime.GameData.User
{
    public enum RulesType
    {
        [InspectorName("守序")] Lawful = (0x01 << 0) + 0x38,
        [InspectorName("逍遥")] Carefree = (0x01 << 1) + 0x38,
        [InspectorName("混乱")] Chaos = (0x01 << 2) + 0x38,
    }
}
 