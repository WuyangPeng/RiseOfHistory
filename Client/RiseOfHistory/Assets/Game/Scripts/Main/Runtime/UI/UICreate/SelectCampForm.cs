using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.GameData.User;
using Game.Scripts.Main.Runtime.GameEnum;
using Game.Scripts.Main.Runtime.GameModule.Base.User;
using Game.Scripts.Main.Runtime.Procedure.Scene;
using Game.Scripts.Main.Runtime.UI.UICommon;
using Game.Scripts.Main.Runtime.UI.UICreate.Display;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;

namespace Game.Scripts.Main.Runtime.UI.UICreate
{
    public class SelectCampForm : UGuiForm
    {
        private ProcedureCreate procedureCreate = null;

        [SerializeField]
        private CampDisplay campDisplay;

        [SerializeField]
        private Toggle[] rulesToggle = null;

        [SerializeField]
        private Toggle[] moralityToggle = null;

        public void OnLeftRulesButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetRulesType(RulesType.Lawful);
        }

        public void OnMiddleRulesButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetRulesType(RulesType.Carefree);
        }

        public void OnRightRulesButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetRulesType(RulesType.Chaos);
        }



        public void OnLeftMoralityButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetMoralityType(MoralityType.Benevolence);
        }

        public void OnMiddleMoralityButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetMoralityType(MoralityType.Moderation);
        }

        public void OnRightMoralityButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetMoralityType(MoralityType.Craftiness);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            procedureCreate = (ProcedureCreate)GetCurrentProcedure();

            if (procedureCreate == null)
            {
                Log.Warning("ProcedureCreate is invalid when open SelectPersonalityForm.");
            }

            campDisplay.Refresh();

            InitCamp();
        }

        private void InitCamp()
        {
            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();

            var rulesType = (int)userModule.GetRulesType();

            for (var i = 0; i < rulesToggle.Length; i++)
            {
                if ((rulesType & (1 << i)) != 0)
                {
                    rulesToggle[i].isOn = true;
                }
            }

            var moralityType = (int)userModule.GetMoralityType() >> 3;

            for (var i = 0; i < moralityToggle.Length; i++)
            {
                if ((moralityType & (1 << i)) != 0)
                {
                    rulesToggle[i].isOn = true;
                }
            }
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            procedureCreate = null;

            base.OnClose(isShutdown, userData);
        }

        public void OnReturnButtonClick()
        {
            procedureCreate.RemoveUIForm(UIFormId.SelectCampForm);
        }

        public void OnEnterButtonClick()
        {
            procedureCreate.OpenUIForm(UIFormId.SelectRaceForm);
        }

    }
}