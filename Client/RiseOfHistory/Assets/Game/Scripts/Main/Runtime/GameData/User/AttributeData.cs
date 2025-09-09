using Game.Scripts.Main.Runtime.DataTable;
using System.Collections.Generic;
using Game.Scripts.Main.Runtime.Base;

namespace Game.Scripts.Main.Runtime.GameData.User
{
    public class AttributeData
    {
        private readonly Dictionary<BaseAttributeType, int> baseAttribute = new();
        private readonly Dictionary<DefaultAttributeType, int> defaultAttribute = new();

        public void InitAttribute()
        {
            baseAttribute.Clear();
            defaultAttribute.Clear();

            var property = GameEntry.DataTable.GetDataTable<DRProperty>();
        }
    }
}