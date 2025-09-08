using Game.Scripts.Main.Runtime.UI.UICreate.Item;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.UI.UICreate.Display
{
    public class GameSexDisplay : MonoBehaviour
    {
        [SerializeField] private GameSexItem gameSexItem;
        public void Refresh()
        {
            gameSexItem.SetData("Sex.Male", "Sex.Female");
        }
    }
}