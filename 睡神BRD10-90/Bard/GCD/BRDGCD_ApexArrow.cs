using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Morpheus.Bard.GCD;

public class BRDGCD_ApexArrow : ISlotResolver//绝峰箭
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;// 表示这个技能槽是属于 GCD（公共冷却时间）的

    public int Check()// 检查是否需要施放 "Apex Arrow" 技能的方法
    {
        if (!Helper.已解锁(技能.绝峰箭)) return -5;
        
        if (Helper.灵魂之声() < 100 && !Helper.自身存在Buff(Buff.爆破箭预备))// 如果 "Soul Voice" 能量小于100且没有 "Blast Arrow Ready" 光环
        {
            if (!Helper.自身存在Buff大于时间(Buff.战斗之声, 6500) &&  Helper.自身存在自身施加Buff(Buff.战斗之声))// 如果没有 "Battle Voice" 光环，或者 "Battle Voice" 光环剩余时间小于6500毫秒但已经存在
                if (Helper.灵魂之声() >= 80)// 如果 "Soul Voice" 能量大于等于80，返回 1，表示执行这个技能槽
                    return 1;
            return -1;// 如果 "Soul Voice" 能量小于80，返回 -1，表示不执行这个技能槽
        }

        if (SpellsDefine.BattleVoice.CoolDownInGCDs(10)) return -1;// 如果 "Battle Voice" 技能在接下来的10个 GCDs内处于冷却中，返回 -1，表示不执行这个技能槽
        return 0;// 如果以上条件都不满足，返回 0，表示可以执行这个技能槽
    }

    public void Build(Slot slot)// 构建技能槽的方法
    {
        slot.Add(Helper.精通技能(技能.绝峰箭.获取技能().Id).获取技能());// 添加 "Apex Arrow" 技能到技能槽中
    }
}