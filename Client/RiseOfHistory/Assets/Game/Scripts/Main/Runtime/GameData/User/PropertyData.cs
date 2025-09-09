using System.Collections.Generic;
using Game.Scripts.Main.Runtime.GameEnum;

namespace Game.Scripts.Main.Runtime.GameData.User
{
    public class PropertyData
    {
        private readonly Dictionary<BasePropertyType, int> baseProperty = new();
        private readonly Dictionary<DefaultPropertyType, int> defaultProperty = new();

        public int GetBaseProperty(BasePropertyType basePropertyType)
        {
            return baseProperty.GetValueOrDefault(basePropertyType, 0);
        }

        public int GetDefaultProperty(DefaultPropertyType defaultPropertyType)
        {
            return defaultProperty.GetValueOrDefault(defaultPropertyType, 0);
        }

        public void AddBaseProperty(int propertyId)
        {
            baseProperty[(BasePropertyType)propertyId] = GetBaseProperty((BasePropertyType)propertyId) + 1;
        }

        public void ReduceBaseProperty(int propertyId)
        {
            baseProperty[(BasePropertyType)propertyId] = GetBaseProperty((BasePropertyType)propertyId) - 1;
        }
    }
}