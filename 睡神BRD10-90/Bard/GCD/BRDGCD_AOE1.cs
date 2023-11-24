using CombatRoutine;
using Common;
using Common.Define;


namespace Morpheus.Bard.GCD;

public class BRDGCD_AOE1 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;// 表示这个技能槽是属于 GCD（公共冷却时间）的

    public int Check()// 检查触发条件的方法

    {
        if (!Helper.已解锁(技能.连珠箭)) return -5;
        if (!技能.连珠箭.IsReady()) return -10;
        if (Helper.自身周围单位数量(12)<3) return -1;// 
        return 0;
    }

    public void Build(Slot slot)// 构建技能槽的方法
    {
        slot.Add(Helper.精通技能(技能.连珠箭.获取技能().Id).获取技能());
    }

}