using Game.Scripts.Main.Runtime.Base;
using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.GameEnum;
using Game.Scripts.Main.Runtime.GameModule.Base.User;
using Game.Scripts.Main.Runtime.UI.UICreate.Item;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Main.Runtime.UI.UICreate.Display
{
    public class SpiritualDisplay : MonoBehaviour
    {
        [SerializeField] private Radio2Item[] items = null;

        [SerializeField] private Text remainingText;

        public void Refresh()
        {
            var spiritual = GameEntry.DataTable.GetDataTable<DRSpiritual>();
            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();

            var index = 0;
            foreach (var element in spiritual)
            {
                 items[index].SetNum(element.Name, userModule.GetSpiritual((SpiritualType)element.Id));

                ++index;
            }

            remainingText.text = userModule.GetSpiritualCount().ToString();

        }
    }
}