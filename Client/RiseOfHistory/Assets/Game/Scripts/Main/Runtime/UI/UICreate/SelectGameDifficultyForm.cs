using Game.Scripts.Main.Runtime.Procedure.Scene;
using Game.Scripts.Main.Runtime.UI.UICommon;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.UI.UICreate
{
    public class SelectGameDifficultyForm : UGuiForm
    {
        private ProcedureCreate procedureCreate = null;

        [SerializeField]
        private GameDifficultyDisplay gameDifficultyDisplay;

        public void OnReturnButtonClick()
        {
            procedureCreate.ReturnMenu();
        }

        public void OnEnterButtonClick(int index)
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

            gameDifficultyDisplay.Refresh();
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            procedureCreate = null;

            base.OnClose(isShutdown, userData);
        }
    }
}