using Game.Scripts.Main.Runtime.SaveData;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.UI.UIMenu
{
    public class HeadDataDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text dateText;
        [SerializeField] private TMP_Text cultivationRealmText;
        [SerializeField] private TMP_Text gameDifficultyType;

        public void SetData(HeadData data)
        {
            titleText.text = data.Name;
            dateText.text = $"第{data.Year}年{data.Month}月";
            cultivationRealmText.text = $"{data.CultivationRealmType.DisplayName()}{data.CultivationRealmLevel}层";
            gameDifficultyType.text = $"{data.GameDifficultyType.DisplayName()}";
        }
    }
}