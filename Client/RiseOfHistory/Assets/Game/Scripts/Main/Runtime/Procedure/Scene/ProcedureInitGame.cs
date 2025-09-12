using Game.Scripts.Main.Runtime.Game;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Game.Scripts.Main.Runtime.Procedure.Scene
{
    public class ProcedureInitGame : ProcedureBase
    {
        public override bool UseNativeDialog => false;

        private const float DelayedSeconds = 2f;

        private float gotoHomeDelaySeconds = 0f;

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
        }

        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            gotoHomeDelaySeconds += elapseSeconds;
            if (gotoHomeDelaySeconds < DelayedSeconds)
            {
                return;
            }

            procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Home"));
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }
    }
}