using System.Runtime.InteropServices;
using Game.Scripts.Main.Runtime.Definition.Enum;
using RiseOfHistory;

namespace Game.Scripts.Main.Runtime.Definition.DataStruct
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct ImpactData
    {
        public ImpactData(CampType camp, int hp, int attack, int defense)
        {
            Camp = camp;
            HP = hp;
            Attack = attack;
            Defense = defense;
        }

        public CampType Camp { get; }

        public int HP { get; }

        public int Attack { get; }

        public int Defense { get; }
    }
}
