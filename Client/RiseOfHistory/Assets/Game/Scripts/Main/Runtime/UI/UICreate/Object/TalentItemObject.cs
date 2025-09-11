using Game.Scripts.Main.Runtime.UI.UICreate.Item;
using GameFramework;
using GameFramework.ObjectPool;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.UI.UICreate.Object
{
    public class TalentItemObject : ObjectBase
    {
        public static TalentItemObject Create(TalentItem item)
        {
            var avatarItemObject = ReferencePool.Acquire<TalentItemObject>();
            avatarItemObject.Initialize(item);
            return avatarItemObject;
        }

        protected override void OnSpawn()
        {
            ((TalentItem)Target).gameObject.SetActive(true);
        }

        protected override void OnUnspawn()
        {
            ((TalentItem)Target).OnRecycle();
            ((TalentItem)Target).gameObject.SetActive(false);
        }

        protected override void Release(bool isShutdown)
        {
            var item = (TalentItem)Target;
            item.OnRecycle();
            UnityEngine.Object.Destroy(item.gameObject);
        }
    }
}