using Game.Scripts.Main.Runtime.GameData.User;
using Game.Scripts.Main.Runtime.GameModule.Base.User;
using Game.Scripts.Main.Runtime.Procedure.Scene;
using Game.Scripts.Main.Runtime.UI.UICommon;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;

namespace Game.Scripts.Main.Runtime.UI.UICreate
{
    public class SelectGameParameterForm : UGuiForm
    {
        private ProcedureCreate procedureCreate = null;

        [SerializeField]
        private GameParameterDisplay gameParameterDisplay;

        public void OnReturnButtonClick()
        {
            procedureCreate.RemoveUIForm(UIFormId.SelectGameParameterForm);
        }

        public void OnEnterButtonClick()
        {

        }

        public void OnSmallMapSizeButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetMapSize(GameParameterType.Small);
        }

        public void OnMiddleMapSizeButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetMapSize(GameParameterType.Middle);
        }

        public void OnBigMapSizeButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetMapSize(GameParameterType.Big);
        }

        public void OnSmallNpcCountButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetNpcCount(GameParameterType.Small);
        }

        public void OnMiddleNpcCountButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetNpcCount(GameParameterType.Middle);
        }


        public void OnBigNpcCountButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetNpcCount(GameParameterType.Big);
        }




        public void OnSmallSectCountButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetSectCount(GameParameterType.Small);
        }

        public void OnMiddleSectCountButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetSectCount(GameParameterType.Middle);
        }
        public void OnBigSectCountButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetSectCount(GameParameterType.Big);
        }

        public void OnSmallFamilyCountButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetFamilyCount(GameParameterType.Small);
        }

        public void OnMiddleFamilyCountButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetFamilyCount(GameParameterType.Middle);
        }

        public void OnBigFamilyCountButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetFamilyCount(GameParameterType.Big);
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