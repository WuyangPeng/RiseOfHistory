using Game.Scripts.Main.Runtime.Base;
using Game.Scripts.Main.Runtime.GameModule.User;
using Game.Scripts.Main.Runtime.GameModule.World; 

namespace Game.Scripts.Main.Runtime.InitGame
{
    public class FamilyInitGame : InitGameBase
    {
        private readonly UserModule userModule = GameEntry.ModuleComponent.GetModule<UserModule>();
        private readonly FamilyModule familyModule = GameEntry.ModuleComponent.GetModule<FamilyModule>();

        public override void InitGame()
        {

        }

        public override void SaveGame()
        {

        }
    }
}