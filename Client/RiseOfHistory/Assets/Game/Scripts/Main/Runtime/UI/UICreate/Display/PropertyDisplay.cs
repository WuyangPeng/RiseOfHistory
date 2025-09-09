using Game.Scripts.Main.Runtime.Base;
using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.GameEnum;
using Game.Scripts.Main.Runtime.GameModule.Base.User;
using Game.Scripts.Main.Runtime.UI.UICreate.Item;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.UI.UICreate.Display
{
    public class PropertyDisplay : MonoBehaviour
    {
        [SerializeField] private Radio2Item[] items = null;

        public void Refresh()
        {
            var property = GameEntry.DataTable.GetDataTable<DRProperty>();

            var index = 0;
            foreach (var element in property)
            {
                if (element.Group != (int)PropertyGroupType.Base) continue;

                var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
                items[index].SetNum(element.Name, userModule.GetBaseProperty((BasePropertyType)element.Id));

                ++index;
            }

        }
    }
}