using Game.Scripts.Main.Runtime.DataTable;
using System.Collections.Generic;
using Game.Scripts.Main.Runtime.Base;
using Game.Scripts.Main.Runtime.GameEnum;

namespace Game.Scripts.Main.Runtime.GameData.User
{
    public class PropertyData
    {
        private readonly Dictionary<BasePropertyType, int> baseProperty = new();
        private readonly Dictionary<DefaultPropertyType, int> defaultProperty = new();

        public void InitAttribute()
        {
            baseProperty.Clear();
            defaultProperty.Clear();

            var property = GameEntry.DataTable.GetDataTable<DRProperty>();
            foreach (var element in property)
            {

            }
        }
    }
}