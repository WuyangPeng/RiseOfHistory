using Game.Scripts.Main.Runtime.SaveData;
using System.ComponentModel;
using System.Reflection;
using System;
using Game.Scripts.Main.Runtime.Base;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.Main.Runtime.GameData.User;
using Game.Scripts.Main.Runtime.DataTable;
using Object = System.Object;
using GameFramework.Resource;

namespace Game.Scripts.Main.Runtime.UI.UIMenu
{
    public class HeadDataItem : MonoBehaviour
    {
        [SerializeField] private Text titleText;
        [SerializeField] private Text dateText;
        [SerializeField] private Text cultivationRealmText;
        [SerializeField] private Text gameDifficultyText;
        [SerializeField] private Image avatarImage;
        [SerializeField] private Text createNewGame;

        public void SetData(HeadData data)
        {
            titleText.text = data.Name;
            var content = GameEntry.Localization.GetString("Date.SaveData");
            dateText.text = string.Format(content, data.Year, data.Month);
            var cultivationRealm = GameEntry.DataTable.GetDataTable<DRCultivationRealm>();
            var cultivationRealmRow = cultivationRealm.GetDataRow((int)data.CultivationRealmType);
            if (cultivationRealmRow != null)
            {
                cultivationRealmText.text = $"{GameEntry.Localization.GetString(cultivationRealmRow.Name)}{data.CultivationRealmLevel}{GameEntry.Localization.GetString("CultivationRealm.Level")}";
            }

            var gameDifficulty = GameEntry.DataTable.GetDataTable<DRGameDifficulty>();
            var gameDifficultyRow = gameDifficulty.GetDataRow((int)data.GameDifficultyType);
            if (gameDifficultyRow != null)
            {
                gameDifficultyText.text = $"{GameEntry.Localization.GetString("GameDifficulty.Description")}:{GameEntry.Localization.GetString(gameDifficultyRow.Name)}";
            }

            var avatar = GameEntry.DataTable.GetDataTable<DRAvatar>();
            var avatarRow = avatar.GetDataRow(10001);
            if (avatarRow != null)
            {
               /* GameEntry.Resource.LoadAsset(avatarRow.Path,new LoadAssetCallbacks(
                    new LoadAssetSuccessCallback(  "",
                          asset,
                          duration,
                          userData)
                    {

                    } */
            }

            createNewGame.gameObject.SetActive(false);
        }

       
    }
}