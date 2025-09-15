using Game.Scripts.Main.Runtime.GameData.World;
using Game.Scripts.Main.Runtime.GameModule.Base;

namespace Game.Scripts.Main.Runtime.GameModule.World
{
    [Module]
    public class NpcModule : BaseModule
    {
        private readonly NpcData npcData = new();

        public long GetNextNpcId()
        {
            return npcData.GetNextNpcId();
        }

        public void AddNpc(NpcBaseData npcBaseData)
        {
            npcData.AddNpc(npcBaseData);
        }

        public int GetNpcCount()
        {
            return npcData.GetNpcCount();
        }

        public NpcData GetNpcData()
        {
            return npcData;
        }
    }
}