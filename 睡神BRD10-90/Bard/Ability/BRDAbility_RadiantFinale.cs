using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Morpheus.Bard.Ability;

public class BRDAbility_RadiantFinale : ISlotResolver//光明神的最后乐章
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;// 表示这个技能槽是属于 OffGCD（非公共冷却时间）的

    public int Check()// 检查是否需要施放 "Radiant Finale" 技能的方法
    {
        if (!技能.光明神的最终乐章.IsReady()) return -10;// 如果 "Radiant Finale" 技能不可用，返回 -10，表示不执行这个技能槽

        if (Core.Get<IMemApiBard>().Coda() < 3) return -1;// 如果 "Coda" 数量小于3，返回 -1，表示不执行这个技能槽

        if (技能.战斗之声.RecentlyUsed()) return 0;// 如果 "Battle Voice" 技能最近被使用，返回 0，表示可以执行这个技能槽
        if (!(技能.战斗之声.GetSpell().Cooldown.TotalMilliseconds < 1000)) return -3;// 如果 "Battle Voice" 技能的冷却时间大于等于1000毫秒，返回 -3，表示不执行这个技能槽
        return 0;// 如果以上条件都不满足，返回 0，表示可以执行这个技能槽
    }

    public void Build(Slot slot)// 构建技能槽的方法
    {
        slot.Add(技能.光明神的最终乐章.GetSpell());// 添加 "Radiant Finale" 技能到技能槽中
        if (技能.战斗之声.IsReady()) slot.Add(SpellsDefine.BattleVoice.GetSpell());// 如果 "Battle Voice" 技能可用，添加到技能槽中
    }
}