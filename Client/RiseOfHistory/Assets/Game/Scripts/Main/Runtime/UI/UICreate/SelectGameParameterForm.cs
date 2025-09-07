using Game.Scripts.Main.Runtime.Procedure.Scene;
using Game.Scripts.Main.Runtime.UI.UICommon;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.UI.UICreate
{
    public class SelectGameParameterForm : UGuiForm
    {
        private ProcedureCreate procedureCreate = null;

        [SerializeField]
        private GameParameterDisplay gameParameterDisplay;
        public void OnReturnButtonClick()
        {
            Close();
        }

        public void OnEnterButtonClick()
        {

        }

        public void OnLeftButtonClick(int index)
        {
           

        }

        public void OnMiddleButtonClick(int index)
        {


        }

        public void OnRightButtonClick(int index)
        {


        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            procedureCreate = (ProcedureCreate)GetCurrentProcedure();

            if (procedureCreate == null)
            {
                Log.Warning("ProcedureCreate is invalid when open SelectGameDifficultyForm.");
            }

            gameParameterDisplay.Refresh();
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            procedureCreate = null;

            base.OnClose(isShutdown, userData);
        }
    }
}