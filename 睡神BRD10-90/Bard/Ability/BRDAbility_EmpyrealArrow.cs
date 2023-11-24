using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Morpheus.Bard.Ability;

public class BRDAbility_EmpyrealArrow : ISlotResolver//九天连箭
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!Helper.已解锁(技能.九天连箭)) return -10;

        if (!Helper.技能可用(技能.九天连箭)) return -9;
        if (!(技能.九天连箭.获取技能().Cooldown.TotalMilliseconds < 1000)) return -8;
        return 0;
    }

    public void Build(Slot slot)
    {
        
            slot.Add(技能.九天连箭.获取技能());
    }
}