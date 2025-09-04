using System.Collections.Generic;

namespace Game.Scripts.Main.Runtime.GameData.User
{
    public class AttributeData
    {
        private readonly Dictionary<BaseAttributeType, int> baseAttribute = new();
        private readonly Dictionary<DefaultAttributeType, int> defaultAttribute = new();
    }
}