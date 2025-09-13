using Game.Scripts.Main.Runtime.Base;
using Game.Scripts.Main.Runtime.GameModule.User;
using Game.Scripts.Main.Runtime.GameModule.World;

namespace Game.Scripts.Main.Runtime.InitGame
{
    public class NpcInitGame : InitGameBase
    {
        private readonly UserModule userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
        private readonly NpcModule npcModule = GameEntry.ModuleComponent.GetModule<NpcModule>();

        public override void InitGame()
        {
            var initNpcCount = userModule.GetInitNpcCount();
            for (var i = 0; i < initNpcCount; ++i)
            {
                
            }
        }

        public override void SaveGame()
        {

        }
    }
}