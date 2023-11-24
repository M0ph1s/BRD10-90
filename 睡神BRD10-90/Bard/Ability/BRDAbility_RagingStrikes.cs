using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Morpheus.Bard.Ability;

public class BRDAbility_RagingStrikes : ISlotResolver//猛者强击
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;// 表示这个技能槽是属于 OffGCD（非公共冷却时间）的

    public int Check()// 检查是否需要施放 "Raging Strikes" 技能的方法
    {
       
        if (!技能.猛者强击.IsReady()) return -10;// 如果 "Raging Strikes" 技能不可用，返回 -10，表示不执行这个技能槽                        没写万一没有贤者歌

        if (!Helper.已解锁(歌.贤者的叙事谣))
                return 2;

        if (Helper.已解锁(歌.放浪神的小步舞曲))
            if (Helper.当前歌曲() != BardSong.TheWanderersMinuet)
                return -8;
        if (!Helper.已解锁(歌.放浪神的小步舞曲))
            if (Helper.当前歌曲() != BardSong.MagesBallad)
                return -7;
        
        

         
        return 1;// 返回 1，表示可以执行这个技能槽
    }

    public void Build(Slot slot)// 构建技能槽的方法
    {
        slot.Add(技能.猛者强击.获取技能());// 添加 "Raging Strikes" 技能到技能槽中
    }
}