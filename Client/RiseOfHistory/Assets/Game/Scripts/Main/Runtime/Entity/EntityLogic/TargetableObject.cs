using Game.Scripts.Main.Runtime.Definition.DataStruct;
using Game.Scripts.Main.Runtime.Entity.EntityData;
using RiseOfHistory;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.Entity.EntityLogic
{
    /// <summary>
    /// 可作为目标的实体类。
    /// </summary>
    public abstract class TargetableObject : Entity
    {
        [SerializeField]
        private TargetableObjectData m_TargetableObjectData = null;

        public bool IsDead => m_TargetableObjectData.Hp <= 0;

        public abstract ImpactData GetImpactData();

        public void ApplyDamage(Entity attacker, int damageHp)
        {
            var fromHpRatio = m_TargetableObjectData.HpRatio;
            m_TargetableObjectData.Hp -= damageHp;
            var toHpRatio = m_TargetableObjectData.HpRatio;
            if (fromHpRatio > toHpRatio)
            {
                Base.GameEntry.HpBar.ShowHPBar(this, fromHpRatio, toHpRatio);
            }

            if (m_TargetableObjectData.Hp <= 0)
            {
                OnDead(attacker);
            }
        }


        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            gameObject.SetLayerRecursively(Definition.Constant.Constant.Layer.TargetableObjectLayerId);
        }


        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_TargetableObjectData = userData as TargetableObjectData;
            if (m_TargetableObjectData != null) return;
            Log.Error("Targetable object data is invalid.");
        }

        protected virtual void OnDead(Entity attacker)
        {
            Base.GameEntry.Entity.HideEntity(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == null || other.gameObject == null)
            {
                return;
            }

            var entity = other.gameObject.GetComponent<Entity>();
            if (entity == null)
            {
                return;
            }

            if (entity is TargetableObject && entity.Id >= Id)
            {
                // 碰撞事件由 Id 小的一方处理，避免重复处理
                return;
            }

            AIUtility.PerformCollision(this, entity);
        }
    }
}
