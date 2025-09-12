using Game.Scripts.Main.Runtime.Sound;
using Game.Scripts.Main.Runtime.UI.UICommon;
using GameFramework.Localization;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.UI.UIMenu
{
    public class SettingForm : UGuiForm
    {
        [SerializeField]
        private Toggle musicMuteToggle;

        [SerializeField]
        private Slider musicVolumeSlider;

        [SerializeField]
        private Toggle soundMuteToggle;

        [SerializeField]
        private Slider soundVolumeSlider;

        [SerializeField]
        private Toggle uiSoundMuteToggle = null;

        [SerializeField]
        private Slider uiSoundVolumeSlider = null;

        [SerializeField]
        private CanvasGroup languageTipsCanvasGroup = null;

        [SerializeField]
        private Toggle englishToggle = null;

        [SerializeField]
        private Toggle chineseSimplifiedToggle = null;

        [SerializeField]
        private Toggle chineseTraditionalToggle = null;

        [SerializeField]
        private Toggle koreanToggle = null;

        private Language m_SelectedLanguage = Language.Unspecified;

        public void OnMusicMuteChanged(bool isOn)
        {
            Base.GameEntry.Sound.Mute("Music", !isOn);
            musicVolumeSlider.gameObject.SetActive(isOn);
        }

        public void OnMusicVolumeChanged(float volume)
        {
            Base.GameEntry.Sound.SetVolume("Music", volume);
        }

        public void OnSoundMuteChanged(bool isOn)
        {
            Base.GameEntry.Sound.Mute("Sound", !isOn);
            soundVolumeSlider.gameObject.SetActive(isOn);
        }

        public void OnSoundVolumeChanged(float volume)
        {
            Base.GameEntry.Sound.SetVolume("Sound", volume);
        }

        public void OnUISoundMuteChanged(bool isOn)
        {
            Base.GameEntry.Sound.Mute("UISound", !isOn);
            uiSoundVolumeSlider.gameObject.SetActive(isOn);
        }

        public void OnUISoundVolumeChanged(float volume)
        {
            Base.GameEntry.Sound.SetVolume("UISound", volume);
        }

        public void OnEnglishSelected(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            m_SelectedLanguage = Language.English;
            RefreshLanguageTips();
        }

        public void OnChineseSimplifiedSelected(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            m_SelectedLanguage = Language.ChineseSimplified;
            RefreshLanguageTips();
        }

        public void OnChineseTraditionalSelected(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            m_SelectedLanguage = Language.ChineseTraditional;
            RefreshLanguageTips();
        }

        public void OnKoreanSelected(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            m_SelectedLanguage = Language.Korean;
            RefreshLanguageTips();
        }

        public void OnSubmitButtonClick()
        {
            if (m_SelectedLanguage == Base.GameEntry.Localization.Language)
            {
                Close();
                return;
            }

            Base.GameEntry.Setting.SetString(Definition.Constant.Constant.Setting.Language, m_SelectedLanguage.ToString());
            Base.GameEntry.Setting.Save();

            Base.GameEntry.Sound.StopMusic();
            GameEntry.Shutdown(ShutdownType.Restart);
        }


        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            musicMuteToggle.isOn = !Base.GameEntry.Sound.IsMuted("Music");
            musicVolumeSlider.value = Base.GameEntry.Sound.GetVolume("Music");

            soundMuteToggle.isOn = !Base.GameEntry.Sound.IsMuted("Sound");
            soundVolumeSlider.value = Base.GameEntry.Sound.GetVolume("Sound");

            uiSoundMuteToggle.isOn = !Base.GameEntry.Sound.IsMuted("UISound");
            uiSoundVolumeSlider.value = Base.GameEntry.Sound.GetVolume("UISound");

            m_SelectedLanguage = Base.GameEntry.Localization.Language;
            switch (m_SelectedLanguage)
            {
                case Language.English:
                    englishToggle.isOn = true;
                    break;

                case Language.ChineseSimplified:
                    chineseSimplifiedToggle.isOn = true;
                    break;

                case Language.ChineseTraditional:
                    chineseTraditionalToggle.isOn = true;
                    break;

                case Language.Korean:
                    koreanToggle.isOn = true;
                    break;

                default:
                    break;
            }
        }


        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            if (languageTipsCanvasGroup.gameObject.activeSelf)
            {
                languageTipsCanvasGroup.alpha = 0.5f + 0.5f * Mathf.Sin(Mathf.PI * Time.time);
            }
        }

        private void RefreshLanguageTips()
        {
            languageTipsCanvasGroup.gameObject.SetActive(m_SelectedLanguage != Base.GameEntry.Localization.Language);
        }
    }
}
