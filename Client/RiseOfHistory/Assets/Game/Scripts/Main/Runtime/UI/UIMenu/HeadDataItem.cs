using Game.Scripts.Main.Runtime.SaveData;
using System.ComponentModel;
using System.Reflection;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.Main.Runtime.GameData.User;

namespace Game.Scripts.Main.Runtime.UI.UIMenu
{
    public class HeadDataItem : MonoBehaviour
    {
        [SerializeField] private Text titleText;
        [SerializeField] private Text dateText;
        [SerializeField] private Text cultivationRealmText;
        [SerializeField] private Text gameDifficultyText;
        [SerializeField] private Text createNewGame;

        public static string GetDescription(CultivationRealmType value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
            return attr == null ? value.ToString() : attr.Description;
        }

        public static string GetDescription(GameDifficultyType value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
            return attr == null ? value.ToString() : attr.Description;
        }

        public void SetData(HeadData data)
        {
            titleText.text = data.Name;
            dateText.text = $"第{data.Year}年{data.Month}月";
            cultivationRealmText.text = $"{GetDescription(data.CultivationRealmType)}{data.CultivationRealmLevel}层";
            gameDifficultyText.text = $"{GetDescription(data.GameDifficultyType)}";

            createNewGame.gameObject.SetActive(false);
        }
    }
}