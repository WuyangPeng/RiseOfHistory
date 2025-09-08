using Game.Scripts.Main.Runtime.SaveData;
using GameFramework.Resource;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;

namespace Game.Scripts.Main.Runtime.UI.UICreate
{
    public class AvatarItem : MonoBehaviour
    {
        [SerializeField] private Image imgBg;
        [SerializeField] private Image imgAvatar;
        [SerializeField] private TextMeshProUGUI txtName;
        private object avatarHandle;          // 资源句柄
        private System.Action<int> onClick;     // 回调
        private int myIndex;                    // 自己在列表里的序号

        // 外部调用：设置数据 + 回调
        public void SetData(int index, HeadData data, System.Action<int> clickCallback)
        {
            myIndex = index;
            onClick = clickCallback;

            txtName.text = data.Name;

            // 1. 先卸载旧图
            if (avatarHandle != null)
            {
                GameEntry.Resource.UnloadAsset(avatarHandle);
                avatarHandle = null;
            }

            // 2. 拼路径（必须 Assets/ 开头）
            string path = $"Assets/Game/Textures/Avatar/{data.Avatar}.png";

            // 3. 异步加载
            GameEntry.Resource.LoadAsset(path, typeof(Sprite), 0,
                new LoadAssetCallbacks(
                     (assetName, asset, duration, userData) =>
                    {
                        avatarHandle = asset;
                        imgAvatar.sprite = asset as Sprite;
                    },
                    (assetName, status, errorMessage, userData) =>
                    {
                        Log.Error($"头像加载失败:{errorMessage}");
                    }));
        }

        // 选中状态
        public void SetSelected(bool selected)
        {
            imgBg.color = selected ? Color.yellow : Color.white;
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
            imgAvatar.sprite = null;
            onClick = null;
        }
    }
}