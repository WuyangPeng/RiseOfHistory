using Game.Scripts.Main.Runtime.GameData.User;

namespace Game.Scripts.Main.Runtime.GameModule.Base.User
{
    [Module]
    public class UserModule : BaseModule
    {
        private readonly UserData userData = new UserData();
        private AttributeData attributeData = new AttributeData();

        public void SetGameDifficulty(GameDifficultyType gameDifficulty)
        {
            userData.GameDifficultyType = gameDifficulty;
        }
    }
}
