using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Morpheus.Bard.Ability;

public class BRDAbility_Bloodletter : ISlotResolver//失血箭
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;// 表示这个技能槽是属于 OffGCD（非公共冷却时间）的

    public int Check()// 检查是否需要施放 "Bloodletter" 技能的方法
    {
        if (!Helper.已解锁(技能.失血箭)) return -5;

        if (!Helper.技能可用(技能.失血箭)) return -10; // 如果 "Bloodletter" 技能不可用，返回 -10，表示不执行这个技能槽

        if (Helper.自身周围单位数量(8) > 2) return -5;

        if (Helper.充能层数(技能.失血箭) < 1) return -6;
      
        if (技能.猛者强击.获取技能().Cooldown.TotalSeconds < 30) return -1;// 如果 "猛者强击" 技能的冷却时间小于30秒，返回 -1，表示不执行这个技能槽
        return 0;// 如果以上条件都不满足，返回 0，表示可以执行这个技能槽
    }

    public void Build(Slot slot)// 构建技能槽的方法
    {
        slot.Add(技能.失血箭.获取技能());// 添加 "Bloodletter" 技能到技能槽中
    }
}