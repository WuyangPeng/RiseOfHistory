using Game.Scripts.Main.Runtime.UI.UICommon;
using Game.Scripts.Main.Runtime.UI.UIForm;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;

namespace Game.Scripts.Main.Runtime.Procedure.Scene
{
    public class ProcedureCreate : ProcedureBase
    {

        private readonly FormComponent formComponent = new FormComponent();


        private float gotoMenuDelaySeconds = 0f;
        private const float GameOverDelayedSeconds = 10f;

        public override bool UseNativeDialog => false;



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
            base.OnEnter(procedureOwner); ;

            formComponent.AddForm(UIFormId.BottomForm);
            formComponent.AddForm(UIFormId.UpperForm);
            formComponent.AddForm(UIFormId.LeftForm);
            formComponent.AddForm(UIFormId.RightForm);

            formComponent.OnEnter(procedureOwner);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            formComponent.OnLeave(procedureOwner, isShutdown);
        }


        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            gotoMenuDelaySeconds += elapseSeconds;
            if (!(gotoMenuDelaySeconds >= GameOverDelayedSeconds)) return;
            procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Menu"));
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }
    }
}
