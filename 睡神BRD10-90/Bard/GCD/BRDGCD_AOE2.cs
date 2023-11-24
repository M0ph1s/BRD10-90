using CombatRoutine;
using Common;
using Common.Define;


namespace Morpheus.Bard.GCD;

public class BRDGCD_AOE2 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;// 表示这个技能槽是属于 GCD（公共冷却时间）的

    public int Check()// 检查触发条件的方法

    {
        if (!Helper.已解锁(技能.影噬箭)) return -5;
        if (!Helper.自身存在Buff(Buff.影噬箭预备)) return -1;
        if (Helper.自身周围单位数量(12) < 3) return -1;// 
        return 0;
    }

    public void Build(Slot slot)// 构建技能槽的方法
    {
        slot.Add(技能.影噬箭.获取技能());
    }

}