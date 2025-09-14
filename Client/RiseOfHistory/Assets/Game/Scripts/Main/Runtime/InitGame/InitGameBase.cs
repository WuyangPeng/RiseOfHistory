using System;
using Game.Scripts.Main.Runtime.GameEnum;
using Game.Scripts.Main.Runtime.RuntimeException;

namespace Game.Scripts.Main.Runtime.InitGame
{
    public abstract class InitGameBase
    {
        public abstract void InitGame();
        public abstract void SaveGame();

        public static InitGameBase Create(InitGameType initGameType)
        {
            return initGameType switch
            {
                InitGameType.Map => new MapInitGame(),
                InitGameType.Sect => new SectInitGame(),
                InitGameType.Family => new FamilyInitGame(),
                InitGameType.Npc => new NpcInitGame(),
                InitGameType.MartialArts => new MartialArtsInitGame(),
                InitGameType.End => new NullInitGame(),
                _ => throw new GameException($"InitGameType = {initGameType} is not exist.")
            };
        }
    }
}