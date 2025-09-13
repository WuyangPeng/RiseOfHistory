using Game.Scripts.Main.Runtime.DataTable;
using System.Collections.Generic;
using Game.Scripts.Main.Runtime.Base;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.GameData.World
{
    public class MapChunkData
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int ResourceId { get; set; }

        public int CurrentResource { get; set; }

        private HashSet<int> entity = new();

        public MapChunkData(int x, int y, int resourceId)
        {
            X = x;
            Y = y;
            ResourceId = resourceId;

            var resourceTable = GameEntry.DataTable.GetDataTable<DRResource>();
            CurrentResource = resourceTable.GetDataRow(resourceId).InitValue;
        }
    }
}