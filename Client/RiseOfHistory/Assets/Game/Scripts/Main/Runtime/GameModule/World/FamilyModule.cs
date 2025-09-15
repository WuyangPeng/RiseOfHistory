using Game.Scripts.Main.Runtime.GameData.World;
using Game.Scripts.Main.Runtime.GameModule.Base;

namespace Game.Scripts.Main.Runtime.GameModule.World
{
    [Module]
    public class FamilyModule : BaseModule
    {
        private readonly FamilyData familyData = new();

        public long GetNextFamilyId()
        {
            return familyData.GetNextFamilyId();
        }

        public void AddFamily(FamilyBaseData familyBaseData)
        {
            familyData.AddFamily(familyBaseData);
        }

        public FamilyData GetFamilyData()
        {
            return familyData;
        }
    }
}