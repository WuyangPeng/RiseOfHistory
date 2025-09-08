using GameFramework;
using GameFramework.ObjectPool;
using Object = UnityEngine.Object;

namespace Game.Scripts.Main.Runtime.UI.UICreate
{
    public class AvatarItemObject : ObjectBase
    {

        public static AvatarItemObject Create(AvatarItem item)
        {
            var obj = ReferencePool.Acquire<AvatarItemObject>();
            obj.Initialize(item);
            return obj;
        }

        protected override void OnSpawn()
        {
            ((AvatarItem)Target).gameObject.SetActive(true);
        }

        protected override void OnUnspawn()
        {
            ((AvatarItem)Target).OnRecycle();
            ((AvatarItem)Target).gameObject.SetActive(false);
        }

        protected override void Release(bool isShutdown)
        {
            var item = (AvatarItem)Target;
            item.OnRecycle();
            Object.Destroy(item.gameObject);
            ReferencePool.Release(this);
        }

    }
}