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
    public class SelectRaceForm : UGuiForm
    {
        private ProcedureCreate procedureCreate = null;

        [SerializeField]
        private RaceDisplay raceDisplay;

        [SerializeField]
        private Text raceTitle;


        [SerializeField]
        private Text raceDescriptionTitle;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            procedureCreate = (ProcedureCreate)GetCurrentProcedure();

            if (procedureCreate == null)
            {
                Log.Warning("ProcedureCreate is invalid when open SelectPersonalityForm.");
            }

            raceDisplay.Refresh();

            SetTitle();
        }

        private void SetTitle()
        {
            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            var raceType = userModule.GetRaceType();

            var races = GameEntry.DataTable.GetDataTable<DRRace>();

            var race = races.GetDataRow((int)raceType);

            raceTitle.text = GameEntry.Localization.GetString(race.Name);
            raceDescriptionTitle.text = GameEntry.Localization.GetString(race.Description);
        }

        public void OnSelectHumanButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetRaceType(RaceType.Human);

            SetTitle();
        }

        public void OnSelectDemonButtonClick(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetRaceType(RaceType.Demon);

            SetTitle();
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            procedureCreate = null;

            base.OnClose(isShutdown, userData);
        }

        public void OnReturnButtonClick()
        {
            procedureCreate.RemoveUIForm(UIFormId.SelectRaceForm);
        }

        public void OnEnterButtonClick()
        {
            procedureCreate.OpenUIForm(UIFormId.SelectAttributeForm);
        }

    }
}