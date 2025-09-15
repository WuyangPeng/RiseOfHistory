using Game.Scripts.Main.Runtime.GameData.World;
using Game.Scripts.Main.Runtime.GameModule.Base;

namespace Game.Scripts.Main.Runtime.GameModule.World
{
    [Module]
    public class SectModule : BaseModule
    {
        private readonly SectData sectData = new();

        public long GetNextSectId()
        {
            return sectData.GetNextSectId();
        }

        public void AddSect(SectBaseData sectBaseData)
        {
            sectData.AddSect(sectBaseData);
        }

        public SectData GetSectData()
        {
            return sectData;
        }
    }
}