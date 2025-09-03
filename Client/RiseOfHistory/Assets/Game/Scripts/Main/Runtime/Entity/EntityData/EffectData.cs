using System;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.Entity.EntityData
{
    [Serializable]
    public class EffectData : EntityData
    {
        [SerializeField]
        private float keepTime = 0f;

        public EffectData(int entityId, int typeId)
            : base(entityId, typeId)
        {
            keepTime = 3f;
        }

        public float KeepTime => keepTime;
    }
}
