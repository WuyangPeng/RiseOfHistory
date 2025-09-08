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
            foreach (var obj in activeAvatarItemObject)
            {
                pool.Unspawn(obj);
            }
            activeAvatarItemObject.Clear();

            for (var i = 0; i < dataList.Count; i++)
            {
                var poolObj = pool.Spawn();
                if (poolObj == null)
                {

                    var go = Instantiate(itemPrefab.gameObject, content);
                    var item = go.GetComponent<AvatarItem>();
                    if (item == null)
                    {
                        Log.Error("预制体没挂 AvatarItem");
                        Destroy(go);
                        return;
                    }

                    var obj = AvatarItemObject.Create(item);
                    pool.Register(obj, true);
                    pool.Unspawn(obj);
                    poolObj = pool.Spawn();
                }

                activeAvatarItemObject.Add(poolObj);

                var avatarItem = (AvatarItem)poolObj.Target;
                avatarItem.transform.SetParent(content, false);
                avatarItem.SetData(i, dataList[i], OnItemClick);
                avatarItem.SetSelected(i == selectedIndex);
            }
        }

        private void OnItemClick(int index)
        {
            selectedIndex = index;
            Refresh();
        }
    }
}