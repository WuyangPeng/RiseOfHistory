//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using Game.Scripts.Main.Runtime.Entity;
using UnityEngine;
using UnityGameFramework.Runtime;
using Entity = Game.Scripts.Main.Runtime.Entity.EntityLogic.Entity;

namespace RiseOfHistory
{
    public class HideByBoundary : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            GameObject go = other.gameObject;
            Entity entity = go.GetComponent<Entity>();
            if (entity == null)
            {
                Log.Warning("Unknown GameObject '{0}', you must use entity only.", go.name);
                Destroy(go);
                return;
            }

            Game.Scripts.Main.Runtime.Base.GameEntry.Entity.HideEntity(entity);
        }
    }
}
