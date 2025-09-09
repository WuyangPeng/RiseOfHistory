using Game.Scripts.Main.Runtime.UI.UICreate.Item;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.UI.UICreate.Display
{
    public class GameParameterDisplay : MonoBehaviour
    {
        [SerializeField] private Radio3Item[] items;
        public void Refresh()
        {
            items[0].SetData("MapSize");
            items[1].SetData("NpcCount");
            items[2].SetData("SectCount");
            items[3].SetData("FamilyCount");
        }
    }

}