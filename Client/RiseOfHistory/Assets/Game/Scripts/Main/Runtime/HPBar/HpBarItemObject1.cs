using GameFramework;
using GameFramework.ObjectPool;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.HPBar
{
    public class HpBarItemObject1 : ObjectBase
    {
        public static HpBarItemObject1 Create(object target)
        {
            var hpBarItemObject = ReferencePool.Acquire<HpBarItemObject1>();
            hpBarItemObject.Initialize(target);
            return hpBarItemObject;
        }

        protected override void Release(bool isShutdown)
        {
            var hpBarItem = (HPBarItem)Target;
            if (hpBarItem == null)
            {
                return;
            }

            Object.Destroy(hpBarItem.gameObject);
        }
    }
}
