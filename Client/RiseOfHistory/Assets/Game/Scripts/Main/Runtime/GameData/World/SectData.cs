using System.Collections.Generic;
using Game.Scripts.Main.Runtime.RuntimeException;

namespace Game.Scripts.Main.Runtime.GameData.World
{
    public class SectData 
    {
        private long currentSectId = 0;
        private readonly Dictionary<long, SectBaseData> sectBaseDataContainer = new();

        public long GetNextSectId()
        {
            return ++currentSectId;
        }

        public SectBaseData GetSectBaseData(long id)
        {
            return sectBaseDataContainer.TryGetValue(id, out var sectBaseData) ? sectBaseData : throw new GameException($"sect id = {id} is not exist");
        }

        public void AddSect(SectBaseData sectBaseData)
        {
            sectBaseDataContainer.Add(sectBaseData.ID, sectBaseData);
        }
    }
}