using System.Collections.Generic;
using System.Linq;
using GameFramework.ObjectPool;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.HPBar
{
    public class HpBarComponent : GameFrameworkComponent
    {
        [SerializeField]
        private HpBarItem1 hpBarItem1Template;

        [SerializeField]
        private Transform hpBarInstanceRoot;

        [SerializeField]
        private int instancePoolCapacity = 16;

        private IObjectPool<HpBarItemObject> hpBarItemObjectPool;
        private List<HpBarItem1> activeHpBarItems;
        private Canvas cachedCanvas;

        private void Start()
        {
            if (hpBarInstanceRoot == null)
            {
                Log.Error("You must set HP bar instance root first.");
                return;
            }

            cachedCanvas = hpBarInstanceRoot.GetComponent<Canvas>();
            hpBarItemObjectPool = Base.GameEntry.ObjectPool.CreateSingleSpawnObjectPool<HpBarItemObject>("HPBarItem", instancePoolCapacity);
            activeHpBarItems = new List<HpBarItem1>();
        }

        private void Update()
        {
            for (var i = activeHpBarItems.Count - 1; i >= 0; i--)
            {
                var hpBarItem = activeHpBarItems[i];
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
                activeHpBarItems.Add(hpBarItem);
            }

            hpBarItem.Init(entity, cachedCanvas, fromHpRatio, toHpRatio);
        }

        private void HideHpBar(HpBarItem1 hpBarItem1)
        {
            hpBarItem1.Reset();
            activeHpBarItems.Remove(hpBarItem1);
            hpBarItemObjectPool.Unspawn(hpBarItem1);
        }

        private HpBarItem1 GetActiveHpBarItem(Entity.EntityLogic.Entity entity)
        {
            return entity == null ? null : activeHpBarItems.FirstOrDefault(item => item.Owner == entity);
        }

        private HpBarItem1 CreateHpBarItem(Entity.EntityLogic.Entity entity)
        {
            HpBarItem1 hpBarItem1;
            var hpBarItemObject = hpBarItemObjectPool.Spawn();
            if (hpBarItemObject != null)
            {
                hpBarItem1 = (HpBarItem1)hpBarItemObject.Target;
            }
            else
            {
                hpBarItem1 = Instantiate(hpBarItem1Template);
                var itemTransform = hpBarItem1.GetComponent<Transform>();
                itemTransform.SetParent(hpBarInstanceRoot);
                itemTransform.localScale = Vector3.one;
                hpBarItemObjectPool.Register(HpBarItemObject.Create(hpBarItem1), true);
            }

            return hpBarItem1;
        }
    }
}
