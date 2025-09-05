using Game.Scripts.Main.Runtime.Procedure.Scene;
using Game.Scripts.Main.Runtime.UI.UICommon;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.UI.UIMenu
{
    public class LoadForm : UGuiForm
    {
        private ProcedureMenu procedureMenu = null;
        public void OnReturnButtonClick()
        {
            procedureMenu.RemoveUIForm(UIFormId.LoadForm);
        }

        public void OnEnterButtonClick(int index)
        {
            if (procedureMenu.HasHeadData(index))
            {
                procedureMenu.LoadGame();
            }
            else
            {
                procedureMenu.StartGame();
            }
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            procedureMenu = (ProcedureMenu)GetCurrentProcedure();

            if (procedureMenu == null)
            {
                Log.Warning("ProcedureMenu is invalid when open LoadForm.");
                return;
            }

            procedureMenu.LoadHeadData();
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            procedureMenu = null;

            base.OnClose(isShutdown, userData);
        }
    }
}