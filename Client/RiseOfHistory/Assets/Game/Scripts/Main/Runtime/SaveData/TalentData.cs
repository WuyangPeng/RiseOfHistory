using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.SaveData
{
    public class TalentData
    {
        public HashSet<int> UnlockTalent { get; set; } = new();
    }
}