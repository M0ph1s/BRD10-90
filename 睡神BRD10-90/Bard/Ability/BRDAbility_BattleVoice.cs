using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Morpheus.Bard.Ability;

public class BRDAbility_BattleVoice : ISlotResolver//战斗之声
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!Helper.已解锁(技能.战斗之声)) return -11;
        if (!Helper.技能可用(技能.战斗之声)) return -10;
        if (!(技能.战斗之声.获取技能().Cooldown.TotalMilliseconds < 1000)) return -3;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(技能.战斗之声.获取技能());
    }
}