using Game.Scripts.Main.Runtime.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Main.Runtime.UI.UICreate.Item
{
    public class Radio2Item : MonoBehaviour
    {
        [SerializeField] private Text leftText;
        [SerializeField] private Text rightText;

        public void SetData(string leftKey, string rightKey)
        {
            leftText.text = GameEntry.Localization.GetString(leftKey);
            rightText.text = GameEntry.Localization.GetString(rightKey);
        }

        public void SetNum(string key, int num)
        {
            leftText.text = GameEntry.Localization.GetString(key);
            rightText.text = num.ToString();
        }
    }
}