using System.Collections.Generic;
using Game.Scripts.Main.Runtime.SaveData;
using Game.Scripts.Main.Runtime.UI.UIMenu.Item;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.UI.UIMenu.Display
{
    public class HeadDataDisplay : MonoBehaviour
    {
        [SerializeField] private HeadDataItem[] items;

        public void Refresh(List<HeadData> headData)
        {
            foreach (var data in headData)
            {
                items[data.Index].SetData(data);
                items[data.Index].gameObject.SetActive(true);
            }
        }

        public void ReleaseAsset()
        {
            foreach (var data in items)
            {
                data.ReleaseAsset();
            }
        }
    }
}