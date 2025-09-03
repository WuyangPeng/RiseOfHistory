using System;
using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.Definition.Enum;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.Entity.EntityData
{
    [Serializable]
    public class ArmorData : AccessoryObjectData
    {
        [SerializeField]
        private int maxHp = 0;

        [SerializeField]
        private int defense = 0;

        public ArmorData(int entityId, int typeId, int ownerId, CampType ownerCamp)
            : base(entityId, typeId, ownerId, ownerCamp)
        {
            var dtArmor = Base.GameEntry.DataTable.GetDataTable<DRArmor>();
            var drArmor = dtArmor.GetDataRow(TypeId);
            if (drArmor == null)
            {
                return;
            }

            maxHp = drArmor.MaxHP;
            defense = drArmor.Defense;
        }

        /// <summary>
        /// 最大生命。
        /// </summary>
        public int MaxHp => maxHp;

        /// <summary>
        /// 防御力。
        /// </summary>
        public int Defense => defense;
    }
}
