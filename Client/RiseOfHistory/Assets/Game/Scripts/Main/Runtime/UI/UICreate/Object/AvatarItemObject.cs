using Game.Scripts.Main.Runtime.UI.UICreate.Item;
using GameFramework;
using GameFramework.ObjectPool;

namespace Game.Scripts.Main.Runtime.UI.UICreate.Object
{
    public class AvatarItemObject : ObjectBase
    {
        public static AvatarItemObject Create(AvatarItem item)
        {
            var avatarItemObject = ReferencePool.Acquire<AvatarItemObject>();
            avatarItemObject.Initialize(item);
            return avatarItemObject;
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
            UnityEngine.Object.Destroy(item.gameObject);
            ReferencePool.Release(this);
        }

    }
}