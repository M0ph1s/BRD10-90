using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Morpheus.Bard.Ability;

public class BRDAbility_Sidewinder : ISlotResolver//侧风诱导箭
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;// 表示这个技能槽是属于 OffGCD（非公共冷却时间）的

    public int Check()// 检查是否需要施放 "Sidewinder" 技能的方法
    {
        if (!Helper.已解锁(技能.侧风诱导箭)) return -10;
        if (!Helper.技能可用(技能.侧风诱导箭)) return -9;// 如果 "Sidewinder" 技能不可用，返回 -10，表示不执行这个技能槽
        if (Helper.自身存在Buff(Buff.直线射击预备)) return -8;// 如果角色拥有 "Straighter Shot" 光辉射击效果，返回 -1，表示不执行这个技能槽
        if (!(技能.侧风诱导箭.获取技能().Cooldown.TotalMilliseconds < 1000)) return -3;
        return 0;// 返回 0，表示可以执行这个技能槽
    }

    public void Build(Slot slot)// 构建技能槽的方法
    {
        
            slot.Add(技能.侧风诱导箭.获取技能());// 添加 "Sidewinder" 技能到技能槽中
    }
}