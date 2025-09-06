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

namespace Game.Scripts.Main.Runtime.UI.UIMenu
{
    public class HeadDataItem : MonoBehaviour
    {
        [SerializeField] private Text titleText;
        [SerializeField] private Text dateText;
        [SerializeField] private Text cultivationRealmText;
        [SerializeField] private Text gameDifficultyText;
        [SerializeField] private Text createNewGame;

        public static string GetDescription(GameDifficultyType value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
            return attr == null ? value.ToString() : attr.Description;
        }

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

            createNewGame.gameObject.SetActive(false);
        }
    }
}