using Game.Scripts.Main.Runtime.Base;
using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.Definition.Constant;
using Game.Scripts.Main.Runtime.GameData.World;
using Game.Scripts.Main.Runtime.GameEnum;
using Game.Scripts.Main.Runtime.GameModule.User;
using Game.Scripts.Main.Runtime.GameModule.World;
using Game.Scripts.Main.Runtime.GameUtility;
using Game.Scripts.Main.Runtime.RuntimeException;
using Game.Scripts.Main.Runtime.SaveData;
using System.Collections.Generic;
using System.Text;
using GameFramework;
using Unity.VisualScripting;
using static UnityEngine.Rendering.DebugUI;

namespace Game.Scripts.Main.Runtime.InitGame
{
    public class NpcInitGame : InitGameBase
    {
        private readonly UserModule userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
        private readonly NpcModule npcModule = GameEntry.ModuleComponent.GetModule<NpcModule>();
        private readonly FamilyModule familyModule = GameEntry.ModuleComponent.GetModule<FamilyModule>();
        private readonly Dictionary<SexType, WeightRandom<int>> avatarWeightRandom = new();
        private readonly Dictionary<SexType, WeightRandom<int>> nameWeightRandom = new();
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
            InitName();
            InitFamily();
            InitNpc();
        }

        private void InitName()
        {
            var nameTable = GameEntry.DataTable.GetDataTable<DRName>();
            var maleWeightRandom = new WeightRandom<int>();
            var femaleWeightRandom = new WeightRandom<int>();
            foreach (var element in nameTable)
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

            nameWeightRandom.Add(SexType.Male, maleWeightRandom);
            nameWeightRandom.Add(SexType.Female, femaleWeightRandom);
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

        private void AddExistName(int surname, int name)
        {
            if (!existName.TryGetValue(surname, out var result))
            {
                result = new HashSet<int>();
                existName[surname] = result;
            }

            result.Add(name);
        }

        private bool IsExistName(int surname, int name)
        {
            return existName.TryGetValue(surname, out var result) && result.Contains(name);
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
                if ((element.Group & (int)MoralityType.Empty) != 0 && (element.Group & (int)RulesType.Empty) != 0)
                {
                    campWeightRandom.Add(element.Id, element.Weight);
                }
            }
        }
        private void InitFamily()
        {
            var initNpcCount = userModule.GetInitNpcCount();
            foreach (var element in familyModule.GetFamilies())
            {
                for (var i = 0; i < Constant.Game.FamilyNpcRandomCount; ++i)
                {

                    var sexType = GetSexType();
                    var npcBaseData = new NpcBaseData
                    {
                        ID = npcModule.GetNextNpcId(),
                        SexType = sexType,
                        AvatarId = GetAvatarId(sexType),
                        CampType = (CampType)((campWeightRandom.Roll() - (int)RulesType.Empty) | (element.MoralityType - MoralityType.Empty)),
                        RaceType = element.RaceType,
                        Surname = element.Surname,
                        Name = GetName(element.Surname, sexType),
                    };

                    npcBaseData.Talent.AddRange(talentWeightRandom.RollMultiple(Constant.Game.MaxTalentCount));

                    npcModule.AddNpc(npcBaseData);


                    if (npcModule.GetNpcCount() > initNpcCount)
                    {
                        break;
                    }
                }
            }
        }

        private int GetName(int surname, SexType sexType)
        {
            if (nameWeightRandom.TryGetValue(sexType, out var value))
            {
                if (value.Count == 0)
                {
                    var nameTable = GameEntry.DataTable.GetDataTable<DRName>();

                    foreach (var element in nameTable)
                    {
                        if ((element.Sex & (int)sexType) != 0)
                        {
                            value.Add(element.Id, element.Weight);
                        }
                    }
                }

                var name = value.Roll();
                if (!IsExistName(surname, name))
                {
                    value.Remove(name);
                    return name;
                }
                else
                {
                    var weightRandom = new WeightRandom<int>();
                    var nameTable = GameEntry.DataTable.GetDataTable<DRName>();

                    if (!existName.TryGetValue(surname, out var result))
                    {
                        result = new HashSet<int>();
                    }

                    foreach (var element in nameTable)
                    {
                        if (((element.Sex & (int)sexType) != 0) && !result.Contains(element.Id))
                        {
                            weightRandom.Add(element.Id, element.Weight);
                        }
                    }

                    name = weightRandom.Roll();
                    value.Remove(name);
                    return name;
                }
            }
            else
            {
                throw new GameException($"SexType {sexType} is not exist.");
            }
        }

        private void InitNpc()
        {
            var initNpcCount = userModule.GetInitNpcCount();
            for (var i = npcModule.GetNpcCount(); i < initNpcCount; ++i)
            {
                var sexType = GetSexType();
                var surname = surnameWeightRandom.Roll();
                var npcBaseData = new NpcBaseData
                {
                    ID = npcModule.GetNextNpcId(),
                    SexType = sexType,
                    AvatarId = GetAvatarId(sexType),
                    CampType = (CampType)campWeightRandom.Roll(),
                    RaceType = (RaceType)raceWeightRandom.Roll(),
                    Surname = surname,
                    Name = GetName(surname, sexType),
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
            var fileSystems = GameEntry.FileSystemComponent.CreateFileSystem("GameSaves/" + userModule.GetSaveIndex(), "NpcData.idx");
            var npcSaveData = new NpcSaveData()
            {
                Data = npcModule.GetNpcData()
            };

            var json = Utility.Json.ToJson(npcSaveData);

            fileSystems.WriteFile("GameSaves", Encoding.UTF8.GetBytes(json));
        }
    }
}