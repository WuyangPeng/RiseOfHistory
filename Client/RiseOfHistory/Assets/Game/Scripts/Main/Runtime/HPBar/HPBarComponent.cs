using System.Collections.Generic;
using System.Linq;
using GameFramework.ObjectPool;
using RiseOfHistory;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.HPBar
{
    public class HPBarComponent : GameFrameworkComponent
    {
        [SerializeField]
        private HPBarItem m_HPBarItemTemplate = null;

        [SerializeField]
        private Transform m_HPBarInstanceRoot = null;

        [SerializeField]
        private int m_InstancePoolCapacity = 16;

        private IObjectPool<HpBarItemObject> m_HPBarItemObjectPool = null;
        private List<HPBarItem> m_ActiveHPBarItems = null;
        private Canvas m_CachedCanvas = null;

        private void Start()
        {
            if (m_HPBarInstanceRoot == null)
            {
                Log.Error("You must set HP bar instance root first.");
                return;
            }

            m_CachedCanvas = m_HPBarInstanceRoot.GetComponent<Canvas>();
            m_HPBarItemObjectPool = Base.GameEntry.ObjectPool.CreateSingleSpawnObjectPool<HpBarItemObject>("HPBarItem", m_InstancePoolCapacity);
            m_ActiveHPBarItems = new List<HPBarItem>();
        }

        private void Update()
        {
            for (var i = m_ActiveHPBarItems.Count - 1; i >= 0; i--)
            {
                var hpBarItem = m_ActiveHPBarItems[i];
                if (hpBarItem.Refresh())
                {
                    continue;
                }

                HideHpBar(hpBarItem);
            }
        }

        public void ShowHpBar(Entity.EntityLogic.Entity entity, float fromHpRatio, float toHpRatio)
        {
            if (entity == null)
            {
                Log.Warning("Entity is invalid.");
                return;
            }

            var hpBarItem = GetActiveHpBarItem(entity);
            if (hpBarItem == null)
            {
                hpBarItem = CreateHpBarItem(entity);
                m_ActiveHPBarItems.Add(hpBarItem);
            }

            hpBarItem.Init(entity, m_CachedCanvas, fromHpRatio, toHpRatio);
        }

        private void HideHpBar(HPBarItem hpBarItem)
        {
            hpBarItem.Reset();
            m_ActiveHPBarItems.Remove(hpBarItem);
            m_HPBarItemObjectPool.Unspawn(hpBarItem);
        }

        private HPBarItem GetActiveHpBarItem(Entity.EntityLogic.Entity entity)
        {
            return entity == null ? null : m_ActiveHPBarItems.FirstOrDefault(item => item.Owner == entity);
        }

        private HPBarItem CreateHpBarItem(Entity.EntityLogic.Entity entity)
        {
            HPBarItem hpBarItem = null;
            var hpBarItemObject = m_HPBarItemObjectPool.Spawn();
            if (hpBarItemObject != null)
            {
                hpBarItem = (HPBarItem)hpBarItemObject.Target;
            }
            else
            {
                hpBarItem = Instantiate(m_HPBarItemTemplate);
                var itemTransform = hpBarItem.GetComponent<Transform>();
                itemTransform.SetParent(m_HPBarInstanceRoot);
                itemTransform.localScale = Vector3.one;
                m_HPBarItemObjectPool.Register(HpBarItemObject.Create(hpBarItem), true);
            }

            return hpBarItem;
        }
    }
}
