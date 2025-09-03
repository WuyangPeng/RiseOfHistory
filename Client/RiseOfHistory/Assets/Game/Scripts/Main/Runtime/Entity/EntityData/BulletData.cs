using System;
using Game.Scripts.Main.Runtime.Definition.Enum;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.Entity.EntityData
{
    [Serializable]
    public class BulletData : EntityData
    {
        [SerializeField]
        private int ownerId = 0;

        [SerializeField]
        private CampType ownerCamp;

        [SerializeField]
        private int attack = 0;

        [SerializeField]
        private float speed = 0f;

        public BulletData(int entityId, int typeId, int ownerId, CampType ownerCamp, int attack, float speed)
            : base(entityId, typeId)
        {
            this.ownerId = ownerId;
            this.ownerCamp = ownerCamp;
            this.attack = attack;
            this.speed = speed;
        }

        public int OwnerId => ownerId;

        public CampType OwnerCamp => ownerCamp;

        public int Attack => attack;

        public float Speed => speed;
    }
}
