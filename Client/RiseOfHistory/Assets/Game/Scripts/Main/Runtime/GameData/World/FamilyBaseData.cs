using Game.Scripts.Main.Runtime.GameEnum;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.GameData.World
{
    public class FamilyBaseData  
    {
        public long ID { get; set; }

        public MoralityType MoralityType { get; set; } = MoralityType.Moderation;

        public RaceType RaceType { get; set; } = RaceType.Human; 

        public int Surname { get; set; } = 1;
    }
}