using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.GameData.World
{
    public class MapChunkData
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public int ResourceId { get; set; } = 1;

        public int CurrentResource { get; set; }

        private HashSet<int> entity = new();

        public MapChunkData(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}