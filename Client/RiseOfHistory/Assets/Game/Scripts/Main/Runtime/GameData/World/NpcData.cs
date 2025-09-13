using System.Collections.Generic;
using Game.Scripts.Main.Runtime.Definition.Constant;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.GameData.World
{
    public class NpcData
    {
        private long currentNpcId = Constant.Game.PlayerId;
        private readonly Dictionary<long, NpcBaseData> npcBaseData = new();

        public long GetNextNpcId()
        {
            return ++currentNpcId;
        }
    }
}