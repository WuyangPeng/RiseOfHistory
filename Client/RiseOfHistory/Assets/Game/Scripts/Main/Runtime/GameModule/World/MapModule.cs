using Game.Scripts.Main.Runtime.GameData.World;
using Game.Scripts.Main.Runtime.GameModule.Base;

namespace Game.Scripts.Main.Runtime.GameModule.World
{
    [Module]
    public class MapModule : BaseModule
    {
        private readonly MapData mapData = new();
        public void AddMapChunkData(MapChunkData mapChunkData)
        {
            mapData.AddMapChunkData(mapChunkData);
        }

        public void SetMapSize(int mapSize)
        {
            mapData.MapSize = mapSize;
        }

        public MapData GetMapData()
        {
            return mapData;
        }
    }
}