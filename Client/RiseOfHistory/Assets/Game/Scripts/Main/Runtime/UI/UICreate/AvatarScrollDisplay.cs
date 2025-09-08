using Game.Scripts.Main.Runtime.SaveData;
using System.Collections.Generic;
using GameFramework.ObjectPool;
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

        private IObjectPool<AvatarItemObject> m_Pool;
        private List<HeadData> m_DataList = new();
        private int m_SelectedIndex = -1;

        void Start()
        {
            // 1. 创建对象池
            m_Pool = GameEntry.ObjectPool.CreateSingleSpawnObjectPool<AvatarItemObject>(
                "HeadItemPool",
                poolCapacity,
                30f,
                16);

            // 2. 测试数据（可换成真实存档）
            for (int i = 0; i < 30; i++)
                m_DataList.Add(new HeadData
                {
                    Name = $"存档{i}",
                    Avatar = i % 10,
                    Year = 1,
                    Month = 1
                });

            Refresh();
        }

        void Refresh()
        {
            // 3. 回池所有

            while (m_Pool.Count > 0) m_Pool.Unspawn(m_Pool.Spawn());

            // 4. 生成新项
            for (int i = 0; i < m_DataList.Count; i++)
            {
                var obj = m_Pool.Spawn();
                var item = (AvatarItem)obj.Target;
                item.transform.SetParent(content, false);
                item.SetData(i, m_DataList[i], OnItemClick);
                item.SetSelected(i == m_SelectedIndex);
            }
        }

        void OnItemClick(int index)
        {
            m_SelectedIndex = index;
            Refresh();          // 刷新选中表现
            Log.Info($"选中 {index}");
        }
    }
}