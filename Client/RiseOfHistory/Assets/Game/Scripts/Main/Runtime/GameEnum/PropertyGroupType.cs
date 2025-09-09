using UnityEngine;

namespace Game.Scripts.Main.Runtime.GameEnum
{
    public enum PropertyGroupType  
    {
        [InspectorName("基础")] Strength = 100001,
        [InspectorName("默认")] Agile = 100002,
        [InspectorName("战斗")] Constitution = 100003,
        [InspectorName("内政")] Intelligence = 100004,
    }
}