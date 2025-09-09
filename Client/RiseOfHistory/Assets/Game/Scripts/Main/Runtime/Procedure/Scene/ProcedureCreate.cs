using Game.Scripts.Main.Runtime.UI.UICommon;
using Game.Scripts.Main.Runtime.UI.UIForm;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;
using Game.Scripts.Main.Runtime.GameModule.Base.User;

namespace Game.Scripts.Main.Runtime.Procedure.Scene
{
    public class ProcedureCreate : ProcedureBase
    {
        private readonly FormComponent formComponent = new FormComponent();

        public override bool UseNativeDialog => false;

        private bool isReturnMenu;

        public void ReturnMenu()
        {
            isReturnMenu = true;
        }

        public void OpenUIForm(UIFormId form)
        {
            formComponent.OpenUIForm(form);
        }

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

            formComponent.AddForm(UIFormId.SelectGameDifficultyForm);
            formComponent.OnEnter(procedureOwner);

            isReturnMenu = false;

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.Init();
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            formComponent.OnLeave(procedureOwner, isShutdown);
        }


        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (!isReturnMenu) return;

            procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Menu"));
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }

        public void RemoveUIForm(UIFormId formId)
        {
            formComponent.RemoveUIForm(formId);
        }
    }
}
