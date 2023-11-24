using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Morpheus.Bard.Ability;

public class BRDAbility_Barrage : ISlotResolver//纷乱箭
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;// 表示这个技能槽是属于 OffGCD（非公共冷却时间）的

    public int Check()// 检查是否需要施放 "Barrage" 技能的方法
    {
        if (!Helper.已解锁(技能.纷乱箭)) return -10;
        if (!Helper.技能可用(技能.纷乱箭)) return -9;
        if (!(技能.纷乱箭.获取技能().Cooldown.TotalMilliseconds < 1000)) return -8;
        if (Helper.已解锁(技能.伶牙俐齿) && !Helper.已解锁(技能.烈毒咬箭))
            if(!Helper.敌人存在Buff大于时间(DOT.风1, 3000) || !Helper.敌人存在Buff大于时间(DOT.毒1, 3000)) return -7;// 如果目标没有同时拥有 "StormBite" 和 "CausticBite" 光环，并且这两个光环的剩余时间都小于3000毫秒，返回 -1，表示不执行这个技能槽
        if (Helper.已解锁(技能.烈毒咬箭))
            if (!Helper.敌人存在Buff大于时间(DOT.风2, 3000) || !Helper.敌人存在Buff大于时间(DOT.毒2, 3000)) return -6;
        if (Helper.自身存在Buff(Buff.直线射击预备)) return -5;

        return 0;// 如果以上条件都不满足，返回 0，表示可以执行这个技能槽

    }

    public void Build(Slot slot)// 构建技能槽的方法
    {
            slot.Add(技能.纷乱箭.获取技能());// 添加 "Barrage" 技能到技能槽中
    }
}