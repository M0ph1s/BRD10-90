using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;


namespace Morpheus.Bard.GCD;

public class BRDGCD_Dot1 : ISlotResolver// 表示这个技能槽是属于 GCD（公共冷却时间）的
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;// 获取需要施放的周期性伤害技能

    private Spell GetSpell()
    {
        if (Helper.技能可用(技能.风蚀箭))
        if (!Helper.敌人存在Buff大于时间(DOT.风1, 3000))
            if (Helper.已解锁(技能.风蚀箭) && !Helper.已解锁(技能.狂风蚀箭))
                return Helper.精通技能(技能.风蚀箭.获取技能().Id).获取技能();
        if (!Helper.敌人存在Buff大于时间(DOT.风2, 3000))
            if (Helper.已解锁(技能.狂风蚀箭))
                return Helper.精通技能(技能.风蚀箭.获取技能().Id).获取技能();// 如果目标没有 "StormBite" 光环或光环剩余时间小于3000毫秒，返回 Stormbite 技能
    
        return null;// 如果以上条件都不满足，返回 null
    }

    public int Check()// 检查是否需要施放周期性伤害技能的方法
    {
        if (!Helper.技能可用(技能.风蚀箭)) return -10;
        if (Helper.敌人存在Buff大于时间(DOT.风2, 3000)  || Helper.敌人存在Buff大于时间(DOT.风1, 3000) ) return -1;// 如果目标同时拥有 "StormBite" 和 "CausticBite" 光环，并且光环剩余时间都大于3000毫秒，返回 -1，表示不执行这个技能槽

        return 0;// 如果条件不满足，返回 0，表示可以执行这个技能槽
    }

    public void Build(Slot slot)// 构建技能槽的方法
    {
        var spell = GetSpell();// 获取需要施放的周期性伤害技能
        if (spell == null)// 如果 spell 为 null，表示不需要施放技能，直接返回
            return;
        slot.Add(spell);// 将获取到的技能添加到技能槽中
    }
}