using Game.Scripts.Main.Runtime.GameData.User;
using Game.Scripts.Main.Runtime.SaveData;

namespace Game.Scripts.Main.Runtime.GameModule.Base.User
{
    [Module]
    public class AccountModule : BaseModule
    {
        private AccountData accountData = new AccountData();

        public void Clear()
        {
            accountData.Clear();
        }

        public void SetTalentData(TalentData talentData)
        {
            accountData.SetTalentData(talentData);
        }

        public bool HasTalent(int talentId)
        {
            return accountData.HasTalent(talentId);
        }
    }
}