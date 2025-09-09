using Game.Scripts.Main.Runtime.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Main.Runtime.UI.UICreate.Item
{
    public class Radio3Item : MonoBehaviour
    {
        [SerializeField] private Text leftText;
        [SerializeField] private Text middleText;
        [SerializeField] private Text rightText;

        public void SetData(string key)
        {
            leftText.text = GameEntry.Localization.GetString("Parameter." + key + ".Small");
            middleText.text = GameEntry.Localization.GetString("Parameter." + key + ".Middle");
            rightText.text = GameEntry.Localization.GetString("Parameter." + key + ".Big");
        }
    }
}