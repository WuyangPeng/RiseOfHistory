using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.GameEnum;
using Game.Scripts.Main.Runtime.GameModule.Base.User;
using Game.Scripts.Main.Runtime.Procedure.Scene;
using Game.Scripts.Main.Runtime.UI.UICommon;
using Game.Scripts.Main.Runtime.UI.UICreate.Display;
using Game.Scripts.Main.Runtime.UI.UIMenu;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;

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
            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            if (0 < userModule.GetPropertyCount())
            {
                GameEntry.UI.OpenDialog(new DialogParams()
                {
                    Mode = 1,
                    Title = GameEntry.Localization.GetString("Property.Allocate.Title"),
                    Message = GameEntry.Localization.GetString("Property.Allocate.Content"),
                    OnClickConfirm = delegate (object userData) { },
                });
                return;
            }


            procedureCreate.OpenUIForm(UIFormId.SelectSpiritualForm);
        }

        public void OnReduceButtonClick(int propertyId)
        {
            var property = GameEntry.DataTable.GetDataTable<DRProperty>();

            var row = property.GetDataRow(propertyId);
            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            var baseProperty = userModule.GetBaseProperty((BasePropertyType)propertyId);
            var initBaseProperty = userModule.GetInitBaseProperty((BasePropertyType)propertyId);
            if (baseProperty <= initBaseProperty || userModule.GetPropertyCount() >= 10)
            {
                return;
            }

            userModule.ReduceBaseProperty(propertyId);
            propertyDisplay.Refresh();
        }

        public void OnAddButtonClick(int propertyId)
        {
            var property = GameEntry.DataTable.GetDataTable<DRProperty>();

            var row = property.GetDataRow(propertyId);
            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            var baseProperty = userModule.GetBaseProperty((BasePropertyType)propertyId);
            if (baseProperty >= row.MaxValue || userModule.GetPropertyCount() <= 0)
            {
                return;
            }

            userModule.AddBaseProperty(propertyId);
            propertyDisplay.Refresh();
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