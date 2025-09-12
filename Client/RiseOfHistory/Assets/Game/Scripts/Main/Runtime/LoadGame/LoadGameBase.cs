using Game.Scripts.Main.Runtime.GameEnum;
using Game.Scripts.Main.Runtime.InitGame;
using Game.Scripts.Main.Runtime.RuntimeException;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.LoadGame
{
    public abstract class LoadGameBase 
    {
        public abstract void LoadGame();

        public static LoadGameBase Create(InitGameType initGameType)
        {
            return initGameType switch
            {
                InitGameType.Map => new MapLoadGame(),
                InitGameType.Npc => new NpcLoadGame(),
                InitGameType.Sect => new SectLoadGame(),
                InitGameType.Family => new FamilyLoadGame(),
                InitGameType.MartialArts => new MartialArtsLoadGame(),
                InitGameType.End => new NullLoadGame(),
                _ => throw new GameException($"InitGameType = {initGameType} is not exist.")
            };
        }
    }
}