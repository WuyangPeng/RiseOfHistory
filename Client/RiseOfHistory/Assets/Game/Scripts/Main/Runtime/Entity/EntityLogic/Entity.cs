using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.Entity.EntityLogic
{
    public abstract class Entity : Plugins.GameFramework.Scripts.Runtime.Entity.EntityLogic
    {
        [SerializeField]
        private EntityData.EntityData entityData = null;

        public int Id => Entity.Id;

        public Animation CachedAnimation
        {
            get;
            private set;
        }


        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            CachedAnimation = GetComponent<Animation>();
        }


        protected override void OnRecycle()
        {
            base.OnRecycle();
        }


        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            entityData = userData as EntityData.EntityData;
            if (entityData == null)
            {
                Log.Error("Entity data is invalid.");
                return;
            }

            Name = Utility.Text.Format("[Entity {0}]", Id);
            CachedTransform.localPosition = entityData.Position;
            CachedTransform.localRotation = entityData.Rotation;
            CachedTransform.localScale = Vector3.one;
        }


        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
        }


        protected override void OnAttached(Plugins.GameFramework.Scripts.Runtime.Entity.EntityLogic childEntity, Transform parentTransform, object userData)
        {
            base.OnAttached(childEntity, parentTransform, userData);
        }


        protected override void OnDetached(Plugins.GameFramework.Scripts.Runtime.Entity.EntityLogic childEntity, object userData)
        {
            base.OnDetached(childEntity, userData);
        }


        protected override void OnAttachTo(Plugins.GameFramework.Scripts.Runtime.Entity.EntityLogic parentEntity, Transform parentTransform, object userData)
        {
            base.OnAttachTo(parentEntity, parentTransform, userData);
        }


        protected override void OnDetachFrom(Plugins.GameFramework.Scripts.Runtime.Entity.EntityLogic parentEntity, object userData)
        {
            base.OnDetachFrom(parentEntity, userData);
        }


        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }
    }
}
