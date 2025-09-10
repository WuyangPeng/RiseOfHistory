using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.GameEnum;
using Game.Scripts.Main.Runtime.GameModule.Base.User;
using Game.Scripts.Main.Runtime.Procedure.Scene;
using Game.Scripts.Main.Runtime.UI.UICommon;
using Game.Scripts.Main.Runtime.UI.UICreate.Display;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.UI.UICreate
{
    public class SelectMartialArtsForm : UGuiForm
    {
        private ProcedureCreate procedureCreate = null;
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            procedureCreate = (ProcedureCreate)GetCurrentProcedure();

            if (procedureCreate == null)
            {
                Log.Warning("ProcedureCreate is invalid when open SelectMartialArtsForm.");
            }


        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            procedureCreate = null;

            base.OnClose(isShutdown, userData);
        }

        public void OnReturnButtonClick()
        {
            procedureCreate.RemoveUIForm(UIFormId.SelectMartialArtsForm);
        }

        public void OnEnterButtonClick()
        {
            procedureCreate.OpenUIForm(UIFormId.SelectTechniqueForm);
        }

        public void OnReduceButtonClick(int spiritualId)
        {

        }

        public void OnAddButtonClick(int spiritualId)
        {

        }
    }
}