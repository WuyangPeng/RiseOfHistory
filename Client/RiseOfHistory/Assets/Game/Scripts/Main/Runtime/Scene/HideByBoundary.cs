using Game.Scripts.Main.Runtime.Entity;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.Scene
{
    public class HideByBoundary : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            GameObject go = other.gameObject;
            Entity.EntityLogic.Entity entity = go.GetComponent<Entity.EntityLogic.Entity>();
            if (entity == null)
            {
                Log.Warning("Unknown GameObject '{0}', you must use entity only.", go.name);
                Destroy(go);
                return;
            }

            global::Game.Scripts.Main.Runtime.Base.GameEntry.Entity.HideEntity(entity);
        }
    }
}
