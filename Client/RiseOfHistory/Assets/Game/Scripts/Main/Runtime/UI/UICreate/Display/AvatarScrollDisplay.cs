using System.Collections.Generic;
using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.UI.UICreate.Item;
using Game.Scripts.Main.Runtime.UI.UICreate.Object;
using GameFramework.ObjectPool;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;

namespace Game.Scripts.Main.Runtime.UI.UICreate.Display
{
    public class AvatarScrollDisplay : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private Transform content;
        [SerializeField] private AvatarItem itemPrefab;
        [SerializeField] private int poolCapacity = 20;

        private IObjectPool<AvatarItemObject> pool;
        private readonly List<DRAvatar> dataList = new();
        private int selectedIndex = -1;
        private readonly List<AvatarItemObject> activeAvatarItemObject = new();

        private void Start()
        {
            pool = GameEntry.ObjectPool.CreateSingleSpawnObjectPool<AvatarItemObject>(
                "AvatarItemPool",
                poolCapacity,
                30f,
                16);

            var avatar = GameEntry.DataTable.GetDataTable<DRAvatar>();
            dataList.AddRange(avatar.GetAllDataRows());

            Refresh();
        }

        private void Refresh()
        {
            UnSpawnAvatar();
            SpawnAvatar();
        }

        private void SpawnAvatar()
        {
            for (var i = 0; i < dataList.Count; i++)
            {
                var spawn = GetSpawn();
                if (spawn == null) return;

                activeAvatarItemObject.Add(spawn);

                var avatarItem = (AvatarItem)spawn.Target;
                avatarItem.transform.SetParent(content, false);
                avatarItem.SetData(i, dataList[i], OnItemClick);
                avatarItem.SetSelected(i == selectedIndex);
            }
        }

        private void UnSpawnAvatar()
        {
            foreach (var obj in activeAvatarItemObject)
            {
                pool.Unspawn(obj);
            }

            activeAvatarItemObject.Clear();
        }

        private AvatarItemObject GetSpawn()
        {
            var result = pool.Spawn();
            if (result != null) return result;

            var go = Instantiate(itemPrefab.gameObject, content);
            if (gameObject.TryGetComponent<AvatarItem>(out var item))
            {
                var obj = AvatarItemObject.Create(item);
                pool.Register(obj, true);
                pool.Unspawn(obj);
                result = pool.Spawn();

                return result;
            }

            Log.Error("预制体没挂 AvatarItem");
            Destroy(go);
            return null;
        }

        private void OnItemClick(int index)
        {
            selectedIndex = index;
            Refresh();
        }
    }
}