using Game.Scripts.Main.Runtime.DataTable;
using GameFramework.Resource;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;

namespace Game.Scripts.Main.Runtime.UI.UICreate.Item
{
    public class AvatarItem : MonoBehaviour
    {
        [SerializeField] private Image imageBackground;
        [SerializeField] private Image imageAvatar;

        private object avatarHandle;
        private System.Action<int> onClick;
        private int myIndex;

        public void SetData(int index, DRAvatar data, System.Action<int> clickCallback)
        {
            myIndex = index;
            onClick = clickCallback;

            if (avatarHandle != null)
            {
                GameEntry.Resource.UnloadAsset(avatarHandle);
                avatarHandle = null;
            }

            GameEntry.Resource.LoadAsset(data.Path, typeof(Sprite), 0,
                new LoadAssetCallbacks(
                     (assetName, asset, duration, userData) =>
                    {
                        avatarHandle = asset;
                        imageAvatar.sprite = asset as Sprite;
                    },
                    (assetName, status, errorMessage, userData) =>
                    {
                        Log.Error($"头像加载失败:{errorMessage}");
                    }));
        }

        public void SetSelected(bool selected)
        {
            imageBackground.color = selected ? Color.yellow : Color.white;
        }

        // 点击按钮
        public void OnItemClick()
        {
            onClick?.Invoke(myIndex);
        }

        // 回池时清理
        public void OnRecycle()
        {
            if (avatarHandle != null)
            {
                GameEntry.Resource.UnloadAsset(avatarHandle);
                avatarHandle = null;
            }
            imageAvatar.sprite = null;
            onClick = null;
        }
    }
}