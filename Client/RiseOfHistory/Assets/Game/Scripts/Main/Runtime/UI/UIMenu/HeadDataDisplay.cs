using Game.Scripts.Main.Runtime.SaveData;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.UI.UIMenu
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
    }
}