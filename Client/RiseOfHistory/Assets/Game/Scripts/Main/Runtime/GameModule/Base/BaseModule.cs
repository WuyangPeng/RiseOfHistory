namespace Game.Scripts.Main.Runtime.GameModule.Base
{
    public abstract class BaseModule
    {
        public abstract ModuleType GetModuleType();

        public virtual bool IsLoad => true;
    }
}
