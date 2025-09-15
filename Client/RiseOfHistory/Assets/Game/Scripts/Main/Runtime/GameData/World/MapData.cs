using Game.Scripts.Main.Runtime.SaveData;
using System.Collections.Generic;
using System.Linq;

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

        public MapChunkData GetMapChunkDataByFamilyId(long familyId)
        {
            return mapChunkContainer.FirstOrDefault(element => element.HasFamily(familyId));
        }

        public MapChunkData GetMapChunkData(int x, int y)
        {
            var index = x + y * MapSize;

            return mapChunkContainer[index];
        }


        public void AddFamilyToRandomChunk(FamilyBaseData familyBaseData)
        {
            var index = UnityEngine.Random.Range(0, mapChunkContainer.Count);
            mapChunkContainer[index].AddFamily(familyBaseData.ID);
        }

        public void AddSectToRandomChunk(SectBaseData sectBaseData)
        {
            var index = UnityEngine.Random.Range(0, mapChunkContainer.Count);
            mapChunkContainer[index].AddSect(sectBaseData.ID);
        }

        public void SetChunkByFamilyId(long entityId, long familyId)
        {
            var mapChunkData = mapChunkContainer.First(element => element.HasFamily(familyId));
            mapChunkData.AddEntity(entityId);
        }

        public void SetChunkBySectId(long entityId, long sectId)
        {
            var mapChunkData = mapChunkContainer.First(element => element.HasSect(sectId));
            mapChunkData.AddEntity(entityId);
        }

        public bool HasEntity(long entityId)
        {
            return mapChunkContainer.Any(element => element.HasFamily(entityId));
        }

        public void AddEntityToRandomChunk(long entityId)
        {
            var index = UnityEngine.Random.Range(0, mapChunkContainer.Count);
            mapChunkContainer[index].AddEntity(entityId);
        }
    }
}