using Game.Scripts.Main.Runtime.Base;
using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.GameEnum;
using Game.Scripts.Main.Runtime.RuntimeException;
using Game.Scripts.Main.Runtime.SaveData;
using GameFramework.Resource;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Main.Runtime.UIItem.UIMenu
{
    public class HeadDataItem : MonoBehaviour
    {
        [SerializeField] 
        private Text titleText;

        [SerializeField] 
        private Text dateText;

        [SerializeField] 
        private Text cultivationRealmText;

        [SerializeField] 
        private Text gameDifficultyText;

        [SerializeField] 
        private Image avatarImage;

        [SerializeField] 
        private Text createNewGame;

        public void SetData(HeadData headData)
        {
            SetTitle(headData);
            SetDate(headData);
            SetCultivationRealmText(headData);
            SetGameDifficultyText(headData);
            SetAvatar(headData);
            HideCreateNewGame();
        }

        private void SetTitle(HeadData headData)
        {
            titleText.text = headData.Name;
        }

        private void SetDate(HeadData headData)
        {
            var content = GameEntry.Localization.GetString("Date.SaveData");
            dateText.text = string.Format(content, headData.Year, headData.Month);
        }

        private void SetCultivationRealmText(HeadData headData)
        {
            var cultivationRealmType = (int)headData.CultivationRealmType;
            var cultivationRealm = GameEntry.DataTable.GetDataTable<DRCultivationRealm>();
            var cultivationRealmRow = cultivationRealm.GetDataRow(cultivationRealmType);
            if (cultivationRealmRow != null)
            {
                cultivationRealmText.text = $"{GameEntry.Localization.GetString(cultivationRealmRow.Name)}{headData.CultivationRealmLevel}{GameEntry.Localization.GetString("CultivationRealm.Level")}";
            }
            else
            {
                throw new GameException($"CultivationRealmType = {cultivationRealmType} not exist.");
            }
        }

        private void SetGameDifficultyText(HeadData headData)
        {
            var gameDifficultyType = (int)headData.GameDifficultyType;
            var gameDifficulty = GameEntry.DataTable.GetDataTable<DRGameDifficulty>();
            var gameDifficultyRow = gameDifficulty.GetDataRow(gameDifficultyType);
            if (gameDifficultyRow != null)
            {
                gameDifficultyText.text = $"{GameEntry.Localization.GetString("GameDifficulty.Description")}:{GameEntry.Localization.GetString(gameDifficultyRow.Name)}";
            }
            else
            {
                throw new GameException($"GameDifficultyType = {gameDifficultyType} not exist.");
            }
        }

        private void SetAvatar(HeadData headData)
        {
            var avatar = GameEntry.DataTable.GetDataTable<DRAvatar>();
            var avatarRow = avatar.GetDataRow(headData.Avatar);
            if (avatarRow != null)
            {
                GameEntry.Resource.LoadAsset(avatarRow.Path, typeof(Sprite), 0,
                    new LoadAssetCallbacks(
                        (assetName, asset, duration, userData) =>
                        {
                            avatarImage.sprite = asset as Sprite;
                        },
                        (assetName, asset, duration, userData) =>
                        {
                            Debug.LogError("LoadAsset " + avatarRow.Path + " error:" + duration);
                        }));
            }
            else
            {
                avatarImage.gameObject.SetActive(false);
            }
        }


        private void HideCreateNewGame()
        {
            createNewGame.gameObject.SetActive(false);
        }

        public void ReleaseAsset()
        {
            if (avatarImage.sprite == null)
            {
                return;
            }

            GameEntry.Resource.UnloadAsset(avatarImage.sprite);
            avatarImage.sprite = null;
        }
    }
}