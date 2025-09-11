using Game.Scripts.Main.Runtime.DataTable;
using GameFramework.Resource;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;

namespace Game.Scripts.Main.Runtime.UI.UICreate.Item
{
    public class TalentItem : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image imageBackground;
        [SerializeField] private Text talentText;

        private object talentHandle;
        private System.Action<int> onClick;
        private int myIndex;

        public void SetData(int index, DRTalent data, System.Action<int> clickCallback)
        {
            myIndex = index;
            onClick = clickCallback;
            talentText.text = GameEntry.Localization.GetString(data.Name);
        }

        public void SetSelected(bool selected)
        {
            imageBackground.color = selected ? Color.blue : Color.yellow;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke(myIndex);
        }

        // 回池时清理
        public void OnRecycle()
        {
            onClick = null;
        }
    }

}