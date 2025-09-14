using Game.Scripts.Main.Runtime.Base;
using Game.Scripts.Main.Runtime.GameModule.User;
using Game.Scripts.Main.Runtime.GameModule.World;

namespace Game.Scripts.Main.Runtime.InitGame
{
    public class SectInitGame : InitGameBase
    {
        private readonly UserModule userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
        private readonly SectModule sectModule = GameEntry.ModuleComponent.GetModule<SectModule>();

        public override void InitGame()
        {

        }

        public override void SaveGame()
        {

        }
    }
}