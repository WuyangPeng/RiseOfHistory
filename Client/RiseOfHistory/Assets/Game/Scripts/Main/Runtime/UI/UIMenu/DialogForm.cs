using Game.Scripts.Main.Runtime.UI.UICommon;
using GameFramework;
using RiseOfHistory;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.UI.UIMenu
{
    public class DialogForm : UGuiForm
    {
        [SerializeField]
        private Text m_TitleText = null;

        [SerializeField]
        private Text m_MessageText = null;

        [SerializeField]
        private GameObject[] m_ModeObjects = null;

        [SerializeField]
        private Text[] m_ConfirmTexts = null;

        [SerializeField]
        private Text[] m_CancelTexts = null;

        [SerializeField]
        private Text[] m_OtherTexts = null;

        private int m_DialogMode = 1;
        private bool m_PauseGame = false;
        private object m_UserData = null;
        private GameFrameworkAction<object> m_OnClickConfirm = null;
        private GameFrameworkAction<object> m_OnClickCancel = null;
        private GameFrameworkAction<object> m_OnClickOther = null;

        public int DialogMode => m_DialogMode;

        public bool PauseGame => m_PauseGame;

        public object UserData => m_UserData;

        public void OnConfirmButtonClick()
        {
            Close();

            m_OnClickConfirm?.Invoke(m_UserData);
        }

        public void OnCancelButtonClick()
        {
            Close();

            m_OnClickCancel?.Invoke(m_UserData);
        }

        public void OnOtherButtonClick()
        {
            Close();

            m_OnClickOther?.Invoke(m_UserData);
        }


        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            var dialogParams = (DialogParams)userData;
            if (dialogParams == null)
            {
                Log.Warning("DialogParams is invalid.");
                return;
            }

            m_DialogMode = dialogParams.Mode;
            RefreshDialogMode();

            m_TitleText.text = dialogParams.Title;
            m_MessageText.text = dialogParams.Message;

            m_PauseGame = dialogParams.PauseGame;
            RefreshPauseGame();

            m_UserData = dialogParams.UserData;

            RefreshConfirmText(dialogParams.ConfirmText);
            m_OnClickConfirm = dialogParams.OnClickConfirm;

            RefreshCancelText(dialogParams.CancelText);
            m_OnClickCancel = dialogParams.OnClickCancel;

            RefreshOtherText(dialogParams.OtherText);
            m_OnClickOther = dialogParams.OnClickOther;
        }


        protected override void OnClose(bool isShutdown, object userData)
        {
            if (m_PauseGame)
            {
                global::Game.Scripts.Main.Runtime.Base.GameEntry.Base.ResumeGame();
            }

            m_DialogMode = 1;
            m_TitleText.text = string.Empty;
            m_MessageText.text = string.Empty;
            m_PauseGame = false;
            m_UserData = null;

            RefreshConfirmText(string.Empty);
            m_OnClickConfirm = null;

            RefreshCancelText(string.Empty);
            m_OnClickCancel = null;

            RefreshOtherText(string.Empty);
            m_OnClickOther = null;

            base.OnClose(isShutdown, userData);
        }

        private void RefreshDialogMode()
        {
            for (var i = 1; i <= m_ModeObjects.Length; i++)
            {
                m_ModeObjects[i - 1].SetActive(i == m_DialogMode);
            }
        }

        private void RefreshPauseGame()
        {
            if (m_PauseGame)
            {
                global::Game.Scripts.Main.Runtime.Base.GameEntry.Base.PauseGame();
            }
        }

        private void RefreshConfirmText(string confirmText)
        {
            if (string.IsNullOrEmpty(confirmText))
            {
                confirmText = global::Game.Scripts.Main.Runtime.Base.GameEntry.Localization.GetString("Dialog.ConfirmButton");
            }

            foreach (var text in m_ConfirmTexts)
            {
                text.text = confirmText;
            }
        }

        private void RefreshCancelText(string cancelText)
        {
            if (string.IsNullOrEmpty(cancelText))
            {
                cancelText = global::Game.Scripts.Main.Runtime.Base.GameEntry.Localization.GetString("Dialog.CancelButton");
            }

            foreach (var text in m_CancelTexts)
            {
                text.text = cancelText;
            }
        }

        private void RefreshOtherText(string otherText)
        {
            if (string.IsNullOrEmpty(otherText))
            {
                otherText = global::Game.Scripts.Main.Runtime.Base.GameEntry.Localization.GetString("Dialog.OtherButton");
            }

            foreach (var text in m_OtherTexts)
            {
                text.text = otherText;
            }
        }
    }
}
