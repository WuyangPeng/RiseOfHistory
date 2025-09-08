using Game.Scripts.Main.Runtime.DataTable;
using Game.Scripts.Main.Runtime.GameModule.Base.User;
using Game.Scripts.Main.Runtime.UI.UICreate.Item;
using Game.Scripts.Main.Runtime.UI.UICreate.Object;
using GameFramework.ObjectPool;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;

namespace Game.Scripts.Main.Runtime.UI.UICreate.Display
{
    public class AvatarScrollDisplay : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private Transform content;
        [SerializeField] private AvatarItem itemPrefab;
        [SerializeField] private int poolCapacity = 20;

        private IObjectPool<AvatarItemObject> pool;
        private readonly List<DRAvatar> avatarData = new();
        private int selectedIndex = -1;
        private readonly List<AvatarItemObject> activeAvatarItemObject = new();

        private void Start()
        {
            pool = GameEntry.ObjectPool.CreateSingleSpawnObjectPool<AvatarItemObject>(
                "AvatarItemPool",
                poolCapacity,
                30f,
                16);

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            var avatarId = userModule.GetAvatarId();
            var sexType = userModule.GetSexType();
            var avatars = GameEntry.DataTable.GetDataTable<DRAvatar>();
            foreach (var avatar in avatars)
            {
                if ((avatar.Sex & (int)sexType) == 0) continue;

                avatarData.Add(avatar);
                if (avatarId == avatar.Id)
                {
                    selectedIndex = avatarData.Count - 1;
                }
            }

            if (selectedIndex == -1)
            {
                selectedIndex = 0;
                userModule.SetAvatarId(avatarData[0].Id);
            }

            Refresh();
        }

        public void Refresh()
        {
            UnSpawnAvatar();
            SpawnAvatar();
        }

        private void SpawnAvatar()
        {
            for (var i = 0; i < avatarData.Count; i++)
            {
                var spawn = GetSpawn();
                if (spawn == null) return;

                activeAvatarItemObject.Add(spawn);

                var avatarItem = (AvatarItem)spawn.Target;
                avatarItem.transform.SetParent(content, false);
                avatarItem.SetData(i, avatarData[i], OnItemClick);
                avatarItem.SetSelected(i == selectedIndex);
            }
        }

        private void UnSpawnAvatar()
        {
            foreach (var obj in activeAvatarItemObject)
            {
                pool.Unspawn(obj);
            }

            activeAvatarItemObject.Clear();
        }

        private AvatarItemObject GetSpawn()
        {
            var result = pool.Spawn();
            if (result != null) return result;

            var itemGameObject = Instantiate(itemPrefab.gameObject, content);
            if (itemGameObject.TryGetComponent<AvatarItem>(out var item))
            {
                var avatarItemObject = AvatarItemObject.Create(item);
                pool.Register(avatarItemObject, true);
                pool.Unspawn(avatarItemObject);
                result = pool.Spawn();

                return result;
            }

            Log.Error("预制体没挂 AvatarItem");
            Destroy(itemGameObject);
            return null;
        }

        private void OnItemClick(int index)
        {
            selectedIndex = index;
            Refresh();

            var userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
            userModule.SetAvatarId(avatarData[index].Id);
        }
    }
}