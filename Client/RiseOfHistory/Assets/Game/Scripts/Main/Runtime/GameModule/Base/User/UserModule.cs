namespace Game.Scripts.Main.Runtime.GameModule.Base.User
{
    [Module]
    public class UserModule : BaseModule
    {
        public override ModuleType GetModuleType()
        {
            return ModuleType.User;
        }
    }
}
