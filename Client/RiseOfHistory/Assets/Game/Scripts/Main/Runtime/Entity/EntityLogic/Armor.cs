using Game.Scripts.Main.Runtime.Entity.EntityData;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.Entity.EntityLogic
{
    /// <summary>
    /// 装甲类。
    /// </summary>
    public class Armor : RiseOfHistory.Entity
    {
        private const string AttachPoint = "Armor Point";

        [SerializeField]
        private ArmorData armorData = null;


        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }


        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            armorData = userData as ArmorData;
            if (armorData == null)
            {
                Log.Error("Armor data is invalid.");
                return;
            }

            Base.GameEntry.Entity.AttachEntity(Entity, armorData.OwnerId, AttachPoint);
        }


        protected override void OnAttachTo(UnityGameFramework.Runtime.EntityLogic parentEntity, Transform parentTransform, object userData)
        {
            base.OnAttachTo(parentEntity, parentTransform, userData);

            Name = Utility.Text.Format("Armor of {0}", parentEntity.Name);
            CachedTransform.localPosition = Vector3.zero;
        }
    }
}
