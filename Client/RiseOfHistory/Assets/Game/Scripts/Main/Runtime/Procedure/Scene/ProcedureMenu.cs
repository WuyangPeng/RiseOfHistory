using Game.Scripts.Main.Runtime.Game;
using Game.Scripts.Main.Runtime.UI.UICommon;
using Game.Scripts.Main.Runtime.UI.UIMenu;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Game.Scripts.Main.Runtime.Procedure.Scene
{
    public class ProcedureMenu : ProcedureBase
    {
        private bool m_StartGame = false;
        private bool m_LoadGame = false;
        private MenuForm m_MenuForm = null;

        public override bool UseNativeDialog => false;

        public void LoadGame()
        {
            m_LoadGame = true;
        }

        public void StartGame()
        {
            m_StartGame = true;
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            m_StartGame = false;
            m_LoadGame = false;
            GameEntry.UI.OpenUIForm(UIFormId.MenuForm, this);

            GameEntry.ModuleComponent.ResetModule();
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            if (m_MenuForm == null) return;
            m_MenuForm.Close(true);
            m_MenuForm = null;
        }

        private int GetNextSceneId()
        {
            if (m_LoadGame)
            {
                return GameEntry.Config.GetInt("Scene.Home");
            }

            return m_StartGame ? GameEntry.Config.GetInt("Scene.Create") : 0;
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (!m_StartGame && !m_LoadGame) return;
            procedureOwner.SetData<VarInt32>("NextSceneId", GetNextSceneId());
            procedureOwner.SetData<VarByte>("GameMode", (byte)GameMode.Survival);
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            var ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_MenuForm = (MenuForm)ne.UIForm.Logic;
        }
    }
}
