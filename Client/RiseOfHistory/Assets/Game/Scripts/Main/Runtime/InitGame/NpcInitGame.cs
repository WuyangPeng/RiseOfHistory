using System.Collections.Generic;
using Game.Scripts.Main.Runtime.Base;
using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.Definition.Constant;
using Game.Scripts.Main.Runtime.GameData.World;
using Game.Scripts.Main.Runtime.GameEnum;
using Game.Scripts.Main.Runtime.GameModule.User;
using Game.Scripts.Main.Runtime.GameModule.World;
using Game.Scripts.Main.Runtime.GameUtility;
using Game.Scripts.Main.Runtime.RuntimeException;
using Unity.VisualScripting;
using static UnityEngine.Rendering.DebugUI;

namespace Game.Scripts.Main.Runtime.InitGame
{
    public class NpcInitGame : InitGameBase
    {
        private readonly UserModule userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
        private readonly NpcModule npcModule = GameEntry.ModuleComponent.GetModule<NpcModule>();
        private readonly Dictionary<SexType, WeightRandom<int>> avatarWeightRandom = new();
        private readonly WeightRandom<int> campWeightRandom = new();
        private readonly WeightRandom<int> raceWeightRandom = new();
        private readonly WeightRandom<int> talentWeightRandom = new();
        private readonly WeightRandom<int> surnameWeightRandom = new();
        private readonly Dictionary<int, HashSet<int>> existName = new();

        private void InitAvatar()
        {
            var avatarTable = GameEntry.DataTable.GetDataTable<DRAvatar>();
            var maleWeightRandom = new WeightRandom<int>();
            var femaleWeightRandom = new WeightRandom<int>();
            foreach (var element in avatarTable)
            {
                if ((element.Sex & (int)SexType.Male) != 0)
                {
                    maleWeightRandom.Add(element.Id, element.Weight);
                }

                if ((element.Sex & (int)SexType.Female) != 0)
                {
                    femaleWeightRandom.Add(element.Id, element.Weight);
                }
            }

            avatarWeightRandom.Add(SexType.Male, maleWeightRandom);
            avatarWeightRandom.Add(SexType.Female, femaleWeightRandom);
        }

        public override void InitGame()
        {
            InitExistName();
            InitAvatar();
            InitCamp();
            InitRace();
            InitTalent();
            InitSurname();
            InitNpc();
        }

        private void InitExistName()
        {
            var surname = userModule.GetSurname();
            var name = userModule.GetName();
            if (!existName.TryGetValue(surname, out var result))
            {
                result = new HashSet<int>();
                existName[surname] = result;
            }

            var nameTable = GameEntry.DataTable.GetDataTable<DRName>();
            foreach (var element in nameTable)
            {
                if (GameEntry.Localization.GetString(element.Name) != name)
                {
                    continue;
                }

                result.Add(element.Id);
                break;
            }
        }

        private void InitSurname()
        {
            var surnameTable = GameEntry.DataTable.GetDataTable<DRSurname>();
            foreach (var element in surnameTable)
            {
                surnameWeightRandom.Add(element.Id, element.Weight);
            }
        }

        private void InitTalent()
        {
            var talentTable = GameEntry.DataTable.GetDataTable<DRTalent>();

            foreach (var element in talentTable)
            {
                talentWeightRandom.Add(element.Id, element.Weight);
            }
        }

        private void InitRace()
        {
            var raceTable = GameEntry.DataTable.GetDataTable<DRRace>();

            foreach (var element in raceTable)
            {
                raceWeightRandom.Add(element.Id, element.Weight);
            }
        }

        private void InitCamp()
        {
            var campTable = GameEntry.DataTable.GetDataTable<DRCamp>();

            foreach (var element in campTable)
            {
                if (element.Total)
                {
                    campWeightRandom.Add(element.Id, element.Weight);
                }
            }
        }


        private void InitNpc()
        {
            var initNpcCount = userModule.GetInitNpcCount();
            for (var i = 0; i < initNpcCount; ++i)
            {
                var sexType = GetSexType();
                var npcBaseData = new NpcBaseData
                {
                    ID = npcModule.GetNextNpcId(),
                    SexType = sexType,
                    AvatarId = GetAvatarId(sexType),
                    CampType = (CampType)campWeightRandom.Roll(),
                    RaceType = (RaceType)raceWeightRandom.Roll(),
                    Surname = surnameWeightRandom.Roll(),
                };

                npcBaseData.Talent.AddRange(talentWeightRandom.RollMultiple(Constant.Game.MaxTalentCount));

                npcModule.AddNpc(npcBaseData);
            }
        }

        private int GetAvatarId(SexType sexType)
        {
            return avatarWeightRandom.TryGetValue(sexType, out var value) ? value.Roll() : throw new GameException($"SexType {sexType} is not exist.");
        }

        private static SexType GetSexType()
        {
            return 0.5 <= UnityEngine.Random.Range(0.0f, 1.0f) ? SexType.Female : SexType.Male;
        }

        public override void SaveGame()
        {

        }
    }
}