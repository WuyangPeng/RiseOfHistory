using Game.Scripts.Main.Runtime.RuntimeException;
using System.Collections.Generic; 

namespace Game.Scripts.Main.Runtime.GameData.World
{
    public class FamilyData  
    {
        private long currentFamilyId = 0;
        private readonly Dictionary<long, FamilyBaseData> familyBaseDataContainer = new();

        public long GetNextFamilyId()
        {
            return ++currentFamilyId;
        }

        public FamilyBaseData GetFamilyBaseData(long id)
        {
            return familyBaseDataContainer.TryGetValue(id, out var familyBaseData) ? familyBaseData : throw new GameException($"family id = {id} is not exist");
        }

        public void AddFamily(FamilyBaseData familyBaseData)
        {
            familyBaseDataContainer.Add(familyBaseData.ID, familyBaseData);
        }
    }
}