using System.Collections.Generic;
using Game.Scripts.Main.Runtime.SaveData;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.GameData.User
{
    public class AccountData
    {
        private readonly HashSet<int> unlockTalent = new HashSet<int>();
        private readonly HashSet<int> unlockAchievements = new HashSet<int>();

        public void Clear()
        {
            unlockTalent.Clear();
            unlockAchievements.Clear();
        }

        public void SetTalentData(TalentData talentData)
        {
            unlockTalent.AddRange(talentData.UnlockTalent);
        }

        public bool HasTalent(int talentId)
        {
            return unlockTalent.Contains(talentId);
        }
    }
}