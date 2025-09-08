using Game.Scripts.Main.Runtime.DataTable;
using GameFramework.ObjectPool;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;

namespace Game.Scripts.Main.Runtime.UI.UICreate
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
            while (pool.Count > 0) pool.Unspawn(pool.Spawn());

            for (var i = 0; i < dataList.Count; i++)
            {
                var obj = pool.Spawn();
                var item = (AvatarItem)obj.Target;
                item.transform.SetParent(content, false);
                item.SetData(i, dataList[i], OnItemClick);
                item.SetSelected(i == selectedIndex);
            }
        }

        private void OnItemClick(int index)
        {
            selectedIndex = index;
            Refresh();
        }
    }
}