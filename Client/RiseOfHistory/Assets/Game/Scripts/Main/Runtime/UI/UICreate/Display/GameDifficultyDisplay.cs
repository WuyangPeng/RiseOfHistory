using Game.Scripts.Main.Runtime.Base;
using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.GameData.User;
using Game.Scripts.Main.Runtime.GameEnum;
using Game.Scripts.Main.Runtime.UI.UICreate.Item;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.UI.UICreate.Display
{
    public class GameDifficultyDisplay : MonoBehaviour
    {
        [SerializeField] private GameDifficultyItem[] items;


        public void Refresh()
        {
            var gameDifficulty = GameEntry.DataTable.GetDataTable<DRGameDifficulty>();

            var gameDifficultyType = GameDifficultyType.Mortal;
            foreach (var item in items)
            {
                var data = gameDifficulty.GetDataRow((int)gameDifficultyType);
                if (data != null)
                {
                    item.SetData(data);
                }

                ++gameDifficultyType;
            }

        }
    }
}