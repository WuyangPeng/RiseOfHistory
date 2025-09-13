using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.GameData.World;
using Game.Scripts.Main.Runtime.GameModule.User;
using Game.Scripts.Main.Runtime.GameModule.World;
using UnityGameFramework.Runtime;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;

namespace Game.Scripts.Main.Runtime.InitGame
{
    public class MapInitGame : InitGameBase
    {
        private readonly UserModule userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
        private readonly MapModule mapModule = GameEntry.ModuleComponent.GetModule<MapModule>();

        private DRResourceLevel GetResourceLevel()
        {
            var gameDifficulty = userModule.GetGameDifficultyType();

            var gameDifficultyTable = GameEntry.DataTable.GetDataTable<DRGameDifficulty>();
            var resourceLevelTable = GameEntry.DataTable.GetDataTable<DRResourceLevel>();

            var gameDifficultyRow = gameDifficultyTable.GetDataRow((int)gameDifficulty);
            return resourceLevelTable.GetDataRow(gameDifficultyRow.ResourceLevel);
        }

        public override void InitGame()
        {
            var resourceLevel = GetResourceLevel();
            var initMapSize = userModule.GetInitMapSize();

            mapModule.SetMapSize(initMapSize);

            for (var x = 0; x < initMapSize; ++x)
            {
                for (var y = 0; y < initMapSize; ++y)
                {
                    var mapChunkData = new MapChunkData(x, y);

                    mapModule.AddMapChunkData(mapChunkData);
                }
            }
        }
    }
}