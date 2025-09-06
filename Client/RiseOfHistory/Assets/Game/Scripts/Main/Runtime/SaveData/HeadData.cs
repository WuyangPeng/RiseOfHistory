using Game.Scripts.Main.Runtime.GameData.User;

namespace Game.Scripts.Main.Runtime.SaveData
{
    public class HeadData
    {
        public string Name { get; set; } = "彭武阳";
        public int Year { get; set; } = 1;

        public int Month { get; set; } = 1;

        public int Index { get; set; }

        public int Avatar { get; set; }

        public CultivationRealmType CultivationRealmType { get; set; } = CultivationRealmType.QiRefining;

        public int CultivationRealmLevel { get; set; } = 1;

        public GameDifficultyType GameDifficultyType { get; set; } = GameDifficultyType.Mortal;
    }
}
 