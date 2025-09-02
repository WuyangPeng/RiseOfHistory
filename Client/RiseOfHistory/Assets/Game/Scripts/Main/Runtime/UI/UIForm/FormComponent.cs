using Game.Scripts.Main.Runtime.UI;
using GameFramework.Event;
using RiseOfHistory;
using System.Collections.Generic;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Game.Scripts.Main.Runtime.Form
{
    public class FormComponent
    {
        private readonly List<UIFormId> uiFormId = new();
        private readonly List<UGuiForm> uGuiForm = new();

        public void OnEnter(ProcedureOwner procedureOwner)
        {
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            foreach (var id in uiFormId)
            {
                GameEntry.UI.OpenUIForm(id, this);
            }
        }

        public void AddForm(UIFormId form)
        {
            uiFormId.Add(form);
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            var ne = (UnityGameFramework.Runtime.OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            uGuiForm.Add((UGuiForm)ne.UIForm.Logic);
        }

        public void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            foreach (var form in uGuiForm)
            {
                form.Close(true);
            }

            uGuiForm.Clear();
        }


    }
}