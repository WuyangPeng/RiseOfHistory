using Game.Scripts.Main.Runtime.UI.UICreate.Item;
using GameFramework;
using GameFramework.ObjectPool;

namespace Game.Scripts.Main.Runtime.UIObject.UICreate
{
    public class TalentItemObject : ObjectBase
    {
        public static TalentItemObject Create(TalentItem item)
        {
            var talentItemObject = ReferencePool.Acquire<TalentItemObject>();
            talentItemObject.Initialize(item);
            return talentItemObject;
        }

        protected override void OnSpawn()
        {
            ((TalentItem)Target).gameObject.SetActive(true);
        }

        protected override void OnUnspawn()
        {
            ((TalentItem)Target).gameObject.SetActive(false);
        }

        protected override void Release(bool isShutdown)
        {
            if (Target is not TalentItem item || item == null)
            {
                return;
            } 

            item.OnRecycle();
            UnityEngine.Object.Destroy(item.gameObject);
        }
    }
}