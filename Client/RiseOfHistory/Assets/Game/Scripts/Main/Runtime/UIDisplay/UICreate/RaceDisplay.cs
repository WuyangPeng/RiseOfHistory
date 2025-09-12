using Game.Scripts.Main.Runtime.Base;
using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.GameData.User;
using Game.Scripts.Main.Runtime.GameEnum;
using Game.Scripts.Main.Runtime.GameModule.Base.User;
using Game.Scripts.Main.Runtime.UIItem.UICreate;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.UI.UICreate.Display
{
    public class RaceDisplay : MonoBehaviour
    {
        [SerializeField] private Radio2Item items;

        public void Refresh()
        {
            var race = GameEntry.DataTable.GetDataTable<DRRace>();

            items.SetData(race.GetDataRow((int)RaceType.Human).Name, race.GetDataRow((int)RaceType.Demon).Name);
        }
    }
}