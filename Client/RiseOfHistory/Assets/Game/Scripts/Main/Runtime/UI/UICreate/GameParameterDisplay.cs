using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.GameData.User;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.UI.UICreate
{
    public class GameParameterDisplay : MonoBehaviour
    {
        [SerializeField] private GameParameterItem[] items;
        public void Refresh()
        {
            items[0].SetData("MapSize");
            items[1].SetData("NpcCount");
            items[2].SetData("SectCount");
            items[3].SetData("FamilyCount");

        }
    }

}