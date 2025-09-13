using System.Collections.Generic;

namespace Game.Scripts.Main.Runtime.GameData.World
{
    public class MapData
    {
        public int MapSize { get; set; }
        private readonly List<MapChunkData> mapChunkContainer = new();

        public void AddMapChunkData(MapChunkData mapChunkData)
        {
            mapChunkContainer.Add(mapChunkData);
        }

        public MapChunkData GetMapChunkData(int x, int y)
        {
            var index = x + y * MapSize;

            return mapChunkContainer[index];
        }

       
    }
}