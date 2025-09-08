using Game.Scripts.Main.Runtime.Procedure.Scene;
using Game.Scripts.Main.Runtime.UI.UICommon;
using Game.Scripts.Main.Runtime.UI.UICreate.Display;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.UI.UICreate
{
    public class SelectAvatarForm : UGuiForm
    {
        private ProcedureCreate procedureCreate = null;

        [SerializeField]
        private GameSexDisplay gameSexDisplay;
        public void OnReturnButtonClick()
        {
            procedureCreate.RemoveUIForm(UIFormId.SelectAvatarForm);
        }

        public void OnEnterButtonClick()
        {

        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            procedureCreate = (ProcedureCreate)GetCurrentProcedure();

            if (procedureCreate == null)
            {
                Log.Warning("ProcedureCreate is invalid when open SelectAvatarForm.");
            }

            gameSexDisplay.Refresh();
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            procedureCreate = null;

            base.OnClose(isShutdown, userData);
        }
    }
}