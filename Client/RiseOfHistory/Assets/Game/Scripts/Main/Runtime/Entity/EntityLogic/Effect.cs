using Game.Scripts.Main.Runtime.Entity.EntityData;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.Entity.EntityLogic
{
    /// <summary>
    /// 特效类。
    /// </summary>
    public class Effect : RiseOfHistory.Entity
    {
        [SerializeField]
        private EffectData effectData = null;

        private float elapseSeconds = 0f;


        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            effectData = userData as EffectData;
            if (effectData == null)
            {
                Log.Error("Effect data is invalid.");
                return;
            }

            elapseSeconds = 0f;
        }


        protected override void OnUpdate(float aElapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(aElapseSeconds, realElapseSeconds);

            elapseSeconds += aElapseSeconds;
            if (elapseSeconds >= effectData.KeepTime)
            {
                Base.GameEntry.Entity.HideEntity(this);
            }
        }
    }
}
