using CombatRoutine;
using Common;
using Common.Define;


namespace Morpheus.Bard.GCD;

public class BRDGCD_1 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;// 表示这个技能槽是属于 GCD（公共冷却时间）的

    public int Check()// 检查触发条件的方法
    {
        //有触发不打1
        if (Helper.自身存在Buff(Buff.直线射击预备)) return -1;// 如果存在触发（比如拥有 "Straighter Shot" 光环），返回 -1，表示不执行这个技能槽
        return 0;// 如果没有触发条件，返回 0，表示可以执行这个技能槽
    }

    public void Build(Slot slot)// 构建技能槽的方法
    {
        slot.Add(Helper.精通技能(技能.强力射击.获取技能().Id).获取技能());

    }

}