using Game.Scripts.Main.Runtime.UI.UICreate.Item;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.UI.UICreate.Display
{
    public class GameSexDisplay : MonoBehaviour
    {
        [SerializeField] private Radio2Item radio2Item;
        public void Refresh()
        {
            radio2Item.SetData("Sex.Male", "Sex.Female");
        }
    }
}