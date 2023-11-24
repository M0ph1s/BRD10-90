using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Morpheus.Bard.GCD;

public class BRDGCD_IronJaws : ISlotResolver//伶牙俐齿
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
        if (!Helper.已解锁(技能.伶牙俐齿)) return -5;
        if (Helper.技能刚使用过(技能.伶牙俐齿,2500)) return -1;// 如果 "Iron Jaws" 在最近的2500毫秒内已经使用过，返回 -1，表示不执行这个技能槽
        if (Playlist.IsRagingStrikes()) return 0;// 如果正在使用 "Raging Strikes" 技能，返回 0，表示可以执行这个技能槽
        if (Helper.已解锁(技能.烈毒咬箭)) 
            if (!Helper.敌人存在Buff大于时间(DOT.风2, 3000) || !Helper.敌人存在Buff大于时间(DOT.毒2, 3000))// 如果目标没有同时拥有 "StormBite" 和 "CausticBite" 光环，并且这两个光环的剩余时间都小于3000毫秒
            if (Helper.敌人存在Buff(DOT.风2) && Helper.敌人存在Buff(DOT.毒2))// 如果目标同时拥有 "StormBite" 和 "CausticBite" 光环，返回 0，表示可以执行这个技能槽
                return 0;
        if (!Helper.已解锁(技能.烈毒咬箭))
            if (!Helper.敌人存在Buff大于时间(DOT.风1, 3000) || !Helper.敌人存在Buff大于时间(DOT.毒1, 3000))// 如果目标没有同时拥有 "StormBite" 和 "CausticBite" 光环，并且这两个光环的剩余时间都小于3000毫秒
                if (Helper.敌人存在Buff(DOT.风1) && Helper.敌人存在Buff(DOT.毒1))// 如果目标同时拥有 "StormBite" 和 "CausticBite" 光环，返回 0，表示可以执行这个技能槽
                    return 0;
        return -1;// 如果以上条件都不满足，返回 -1，表示不执行这个技能槽
    }

    public void Build(Slot slot)// 构建技能槽的方法
    {
        slot.Add(技能.伶牙俐齿.获取技能());// 添加 "Iron Jaws" 技能到技能槽中
    }
}