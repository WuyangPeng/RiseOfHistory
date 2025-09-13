using Game.Scripts.Main.Runtime.Base;
using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.GameData.World;
using Game.Scripts.Main.Runtime.GameEnum;
using Game.Scripts.Main.Runtime.GameModule.User;
using Game.Scripts.Main.Runtime.GameModule.World;
using Game.Scripts.Main.Runtime.GameUtility;
using ProtoBuf.Meta;

namespace Game.Scripts.Main.Runtime.InitGame
{
    public class NpcInitGame : InitGameBase
    {
        private readonly UserModule userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
        private readonly NpcModule npcModule = GameEntry.ModuleComponent.GetModule<NpcModule>();

        public override void InitGame()
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
                    CampType = GetCampType(), 
                };

                npcModule.AddNpc(npcBaseData);
            }
        }

        private static CampType GetCampType()
        {
            var weightRandom = new WeightRandom<DRCamp>();
            var avatarTable = GameEntry.DataTable.GetDataTable<DRCamp>();
            foreach (var element in avatarTable)
            {
                if (element.Total)
                {
                    weightRandom.Add(element, element.Weight);
                }
            }

            return (CampType)weightRandom.Roll().Id;
        }

        private static int GetAvatarId(SexType sexType)
        {
            var weightRandom = new WeightRandom<DRAvatar>();
            var avatarTable = GameEntry.DataTable.GetDataTable<DRAvatar>();
            foreach (var element in avatarTable)
            {
                if ((element.Sex & (int)sexType) != 0)
                {
                    weightRandom.Add(element, element.Weight);
                }
            }

            return weightRandom.Roll().Id;
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