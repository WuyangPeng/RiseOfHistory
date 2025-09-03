using System;
using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.Definition.Enum;
using RiseOfHistory;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.Entity.EntityData
{
    [Serializable]
    public class AsteroidData : TargetableObjectData
    {
        [SerializeField]
        private int maxHp = 0;

        [SerializeField]
        private int attack = 0;

        [SerializeField]
        private float speed = 0f;

        [SerializeField]
        private float angularSpeed = 0f;

        [SerializeField]
        private int deadEffectId = 0;

        [SerializeField]
        private int deadSoundId = 0;

        public AsteroidData(int entityId, int typeId)
            : base(entityId, typeId, CampType.Neutral)
        {
            var dtAsteroid = Base.GameEntry.DataTable.GetDataTable<DRAsteroid>();
            var drAsteroid = dtAsteroid.GetDataRow(TypeId);
            if (drAsteroid == null)
            {
                return;
            }

            Hp = maxHp = drAsteroid.MaxHP;
            attack = drAsteroid.Attack;
            speed = drAsteroid.Speed;
            angularSpeed = drAsteroid.AngularSpeed;
            deadEffectId = drAsteroid.DeadEffectId;
            deadSoundId = drAsteroid.DeadSoundId;
        }

        public override int MaxHp => maxHp;

        public int Attack => attack;

        public float Speed => speed;

        public float AngularSpeed => angularSpeed;

        public int DeadEffectId => deadEffectId;

        public int DeadSoundId => deadSoundId;
    }
}
