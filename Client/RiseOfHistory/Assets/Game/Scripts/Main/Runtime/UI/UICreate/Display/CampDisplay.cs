using Game.Scripts.Main.Runtime.Base;
using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.GameData.User;
using Game.Scripts.Main.Runtime.GameEnum;
using Game.Scripts.Main.Runtime.UI.UICreate.Item;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.UI.UICreate.Display
{
    public class CampDisplay : MonoBehaviour
    {
        [SerializeField] private Radio3Item[] items;
        public void Refresh()
        {
            var camp = GameEntry.DataTable.GetDataTable<DRCamp>();


            items[0].SetData(camp.GetDataRow((int)RulesType.Lawful).Name, camp.GetDataRow((int)RulesType.Carefree).Name, camp.GetDataRow((int)RulesType.Chaos).Name);
            items[1].SetData(camp.GetDataRow((int)MoralityType.Benevolence).Name, camp.GetDataRow((int)MoralityType.Moderation).Name, camp.GetDataRow((int)MoralityType.Craftiness).Name);
        }
    }
}