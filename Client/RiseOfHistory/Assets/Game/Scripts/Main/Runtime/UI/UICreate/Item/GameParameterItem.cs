using Game.Scripts.Main.Runtime.Base;
using Game.Scripts.Main.Runtime.DataTable;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Main.Runtime.UI.UICreate
{
    public class GameParameterItem : MonoBehaviour
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