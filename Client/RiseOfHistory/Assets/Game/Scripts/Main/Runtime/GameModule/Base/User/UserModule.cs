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

        public void Init()
        {
            userData.InitGameParameter();
            attributeData.InitAttribute();
        }

        public void SetMapSize(GameParameterType gameParameterType)
        {
            userData.SetMapSize(gameParameterType);
        }

        public void SetNpcCount(GameParameterType gameParameterType)
        {
            userData.SetNpcCount(gameParameterType);
        }

        public void SetSectCount(GameParameterType gameParameterType)
        {
            userData.SetSectCount(gameParameterType);
        }

        public void SetFamilyCount(GameParameterType gameParameterType)
        {
            userData.SetFamilyCount(gameParameterType);
        }

        public int GetInitMapSize()
        {
            return userData.InitMapSize;
        }

        public int GetInitNpcCount()
        {
            return userData.InitNpcCount;
        }

        public int GetInitSectCount()
        {
            return userData.InitSectCount;
        }

        public int GetInitFamilyCount()
        {
            return userData.InitFamilyCount;
        }

        public SexType GetSexType()
        {
            return userData.SexType;
        }

        public void SetSexType(SexType sexType)
        {
            userData.SexType = sexType;
        }

        public void SetAvatarId(int avatarId)
        {
            userData.AvatarId = avatarId;
        }

        public int GetAvatarId()
        {
            return userData.AvatarId;
        }

        public void SetRulesType(RulesType rulesType)
        {
            userData.SetRulesType(rulesType);
        }

        public void SetMoralityType(MoralityType moralityType)
        {
            userData.SetMoralityType(moralityType);
        }

        public RulesType GetRulesType()
        {
            return userData.GetRulesType();
        }

        public MoralityType GetMoralityType()
        {
            return userData.GetMoralityType();
        }

        public RaceType GetRaceType()
        {
            return userData.RaceType;
        }

        public void SetRaceType(RaceType raceType)
        {
            userData.RaceType = raceType;
        }
    }
}
