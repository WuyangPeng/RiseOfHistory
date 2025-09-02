using RiseOfHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.GameModule.Base
{
    public class ModuleComponent : GameFrameworkComponent
    {
        private readonly Dictionary<ModuleType, BaseModule> modules = new();
        public void InitModule()
        {
            var attributes = ScanWithAttribute();
            foreach (var instance in attributes.Select(attribute => (BaseModule)Activator.CreateInstance(attribute)))
            {
                modules.Add(instance.GetModuleType(), instance);
            }
        }

        public static List<Type> ScanWithAttribute()
        {
            var list = new List<Type>();

            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (asm.IsDynamic) continue;

                try
                {
                    var found = asm.GetTypes()
                        .Where(t => t.IsSubclassOf(typeof(BaseModule)) &&
                                    t.GetCustomAttribute<ModuleAttribute>() != null);
                    list.AddRange(found);
                }
                catch (Exception ex)
                {
                    Log.Warning(ex);
                }
            }

            return list;
        }

        public BaseModule GetBaseModule(ModuleType moduleType)
        {
            return modules[moduleType];
        }
    }
}
