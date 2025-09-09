using Game.Scripts.Main.Runtime.Procedure.Scene;
using Game.Scripts.Main.Runtime.UI.UICommon;
using Game.Scripts.Main.Runtime.UI.UICreate.Display;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.UI.UICreate
{
    public class SelectPropertyForm : UGuiForm
    {
        private ProcedureCreate procedureCreate = null;

        [SerializeField]
        private PropertyDisplay propertyDisplay;

        public void OnReturnButtonClick()
        {
            procedureCreate.RemoveUIForm(UIFormId.SelectPropertyForm);
        }

        public void OnEnterButtonClick()
        {
            procedureCreate.OpenUIForm(UIFormId.SelectSpiritualForm);
        }

        public void OnReduceButtonClick(int propertyId)
        {

        }

        public void OnAddButtonClick(int propertyId)
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

            propertyDisplay.Refresh();
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            procedureCreate = null;

            base.OnClose(isShutdown, userData);
        }
    }
}