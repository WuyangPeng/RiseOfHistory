using System.Collections.Generic;
using Game.Scripts.Main.Runtime.Definition.DataStruct;
using Game.Scripts.Main.Runtime.Entity.EntityData;
using GameFramework;
using RiseOfHistory;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.Entity.EntityLogic
{
    /// <summary>
    /// 战机类。
    /// </summary>
    public abstract class Aircraft : TargetableObject
    {
        [SerializeField]
        private AircraftData m_AircraftData = null;

        [SerializeField]
        protected Thruster m_Thruster = null;

        [SerializeField]
        protected List<Weapon> m_Weapons = new List<Weapon>();

        [SerializeField]
        protected List<Armor> m_Armors = new List<Armor>();


        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_AircraftData = userData as AircraftData;
            if (m_AircraftData == null)
            {
                Log.Error("Aircraft data is invalid.");
                return;
            }

            Name = Utility.Text.Format("Aircraft ({0})", Id);

            Base.GameEntry.Entity.ShowThruster(m_AircraftData.GetThrusterData());

            var weaponDatas = m_AircraftData.GetAllWeaponDatas();
            foreach (var data in weaponDatas)
            {
                Base.GameEntry.Entity.ShowWeapon(data);
            }

            var armorDatas = m_AircraftData.GetAllArmorDatas();
            foreach (var data in armorDatas)
            {
                Base.GameEntry.Entity.ShowArmor(data);
            }
        }


        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
        }


        protected override void OnAttached(UnityGameFramework.Runtime.EntityLogic childEntity, Transform parentTransform, object userData)
        {
            base.OnAttached(childEntity, parentTransform, userData);

            switch (childEntity)
            {
                case Thruster entity:
                    m_Thruster = entity;
                    return;
                case Weapon weapon:
                    m_Weapons.Add(weapon);
                    return;
                case Armor armor:
                    m_Armors.Add(armor);
                    return;
            }
        }


        protected override void OnDetached(UnityGameFramework.Runtime.EntityLogic childEntity, object userData)
        {
            base.OnDetached(childEntity, userData);

            switch (childEntity)
            {
                case Thruster:
                    m_Thruster = null;
                    return;
                case Weapon weapon:
                    m_Weapons.Remove(weapon);
                    return;
                case Armor armor:
                    m_Armors.Remove(armor);
                    return;
            }
        }

        protected override void OnDead(RiseOfHistory.Entity attacker)
        {
            base.OnDead(attacker);

            Base.GameEntry.Entity.ShowEffect(new EffectData(Base.GameEntry.Entity.GenerateSerialId(), m_AircraftData.DeadEffectId)
            {
                Position = CachedTransform.localPosition,
            });
            Base.GameEntry.Sound.PlaySound(m_AircraftData.DeadSoundId);
        }

        public override ImpactData GetImpactData()
        {
            return new ImpactData(m_AircraftData.Camp, m_AircraftData.Hp, 0, m_AircraftData.Defense);
        }
    }
}
