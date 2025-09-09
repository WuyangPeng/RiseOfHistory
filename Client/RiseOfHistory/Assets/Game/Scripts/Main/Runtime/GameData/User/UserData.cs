using Game.Scripts.Main.Runtime.Base;
using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.GameEnum;
using Game.Scripts.Main.Runtime.RuntimeException;
using GameFramework;

namespace Game.Scripts.Main.Runtime.GameData.User
{
    public class UserData
    {
        public GameDifficultyType GameDifficultyType { get; set; } = GameDifficultyType.Mortal;

        public int InitMapSize { get; set; } = 0;

        public int InitNpcCount { get; set; } = 0;

        public int InitSectCount { get; set; } = 0;

        public int InitFamilyCount { get; set; } = 0;

        public SexType SexType { get; set; } = SexType.Male;

        public int AvatarId { get; set; } = 0;

        public CampType CampType { get; set; } = CampType.CarefreeModeration;

        public RaceType RaceType { get; set; } = RaceType.Human;


        public int PropertyCount { get; set; } = 10;

        private int age;

        public UserData()
        {

        }

        public void InitGameParameter()
        {
            var gameParameter = GameEntry.DataTable.GetDataTable<DRGameParameter>();
            var gameParameterRow = gameParameter.GetDataRow((int)GameParameterType.Middle);
            if (gameParameterRow == null) return;

            InitMapSize = Utility.Random.GetRandom(gameParameterRow.MinMapSize, gameParameterRow.MaxMapSize + 1);
            InitNpcCount = Utility.Random.GetRandom(gameParameterRow.MinNpcCount, gameParameterRow.MaxNpcCount + 1);
            InitSectCount = Utility.Random.GetRandom(gameParameterRow.MinSectCount, gameParameterRow.MaxSectCount + 1);
            InitFamilyCount = Utility.Random.GetRandom(gameParameterRow.MinFamilyCount, gameParameterRow.MaxFamilyCount + 1);
        }

        public void SetMapSize(GameParameterType gameParameterType)
        {
            var gameParameterRow = GetGameParameter(gameParameterType);

            InitMapSize = Utility.Random.GetRandom(gameParameterRow.MinMapSize, gameParameterRow.MaxMapSize + 1);
        }

        public void SetNpcCount(GameParameterType gameParameterType)
        {
            var gameParameterRow = GetGameParameter(gameParameterType);

            InitNpcCount = Utility.Random.GetRandom(gameParameterRow.MinNpcCount, gameParameterRow.MaxNpcCount + 1);
        }

        public void SetSectCount(GameParameterType gameParameterType)
        {
            var gameParameterRow = GetGameParameter(gameParameterType);

            InitSectCount = Utility.Random.GetRandom(gameParameterRow.MinSectCount, gameParameterRow.MaxSectCount + 1);
        }

        public void SetFamilyCount(GameParameterType gameParameterType)
        {
            var gameParameterRow = GetGameParameter(gameParameterType);

            InitFamilyCount = Utility.Random.GetRandom(gameParameterRow.MinFamilyCount, gameParameterRow.MaxFamilyCount + 1);
        }

        public DRGameParameter GetGameParameter(GameParameterType gameParameterType)
        {
            var gameParameter = GameEntry.DataTable.GetDataTable<DRGameParameter>();
            var row = gameParameter.GetDataRow((int)gameParameterType);
            return row ?? throw new GameException(Utility.Text.Format("Can not get game parameter '{0}' from data table.", gameParameterType.ToString()));
        }
        public void SetRulesType(RulesType rulesType)
        {
            CampType = (CampType)((int)RulesType.Empty & (int)CampType & (int)rulesType);
        }

        public void SetMoralityType(MoralityType moralityType)
        {
            CampType = (CampType)((int)MoralityType.Empty & (int)CampType & (int)moralityType);
        }

        public RulesType GetRulesType()
        {
            return (RulesType)((int)RulesType.Empty | (int)CampType);
        }

        public MoralityType GetMoralityType()
        {
            return (MoralityType)((int)MoralityType.Empty | (int)CampType);
        }

        public void ReduceProperty()
        {
            --PropertyCount;
        }

        public void AddProperty()
        {
            ++PropertyCount;
        }
    }
}