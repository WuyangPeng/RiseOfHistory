using System.Collections.Generic;
using Game.Scripts.Main.Runtime.Definition.Constant;
using Game.Scripts.Main.Runtime.RuntimeException;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.GameData.World
{
    public class NpcData
    {
        private long currentNpcId = Constant.Game.PlayerId;
        private readonly Dictionary<long, NpcBaseData> npcBaseDatContainer = new();

        public long GetNextNpcId()
        {
            return ++currentNpcId;
        }

        public NpcBaseData GetNpcBaseData(long id)
        {
            if (npcBaseDatContainer.TryGetValue(id, out var value))
            {
                return value;
            }

            throw new GameException($"npc id = {id} is not exist");
        }

        public void AddNpc(NpcBaseData npcBaseData)
        {
            npcBaseDatContainer.Add(npcBaseData.ID, npcBaseData);
        }
    }
}