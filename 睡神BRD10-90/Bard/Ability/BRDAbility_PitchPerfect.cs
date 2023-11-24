using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Morpheus.Bard.Ability;

public class BRDAbility_PitchPerfect : ISlotResolver//完美音调
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;// 表示这个技能槽是属于 OffGCD（非公共冷却时间）的

    public int Check()// 检查是否需要施放 "Pitch Perfect" 技能的方法
    {
        if (!Helper.已解锁(技能.完美音调)) return -5;
        if (Helper.当前歌曲() != BardSong.TheWanderersMinuet) return -1;// 如果当前激活的歌曲不是 "The Wanderer's Minuet"，返回 -1，表示不执行这个技能槽
        if (Helper.诗心() == 3) return 0;// 如果 "Repertoire" 数量为3，返回 0，表示可以执行这个技能槽
        

        if (Helper.诗心() > 0)// 如果 "Repertoire" 数量大于0
        {
            if (Playlist.IsRagingStrikes()) return 0;// 如果 "Raging Strikes" 技能激活，返回 0，表示可以执行这个技能槽
            if (Core.Get<IMemApiBard>().SongTimer().TotalMilliseconds < 2900) return 0;// 如果当前歌曲的持续时间小于2900毫秒，返回 0，表示可以执行这个技能槽

            if (Helper.诗心() == 3) return 0;// 如果 "Repertoire" 数量为3，返回 0，表示可以执行这个技能槽
            return -2;// 返回 -2，表示不执行这个技能槽
        }

        return -1;// 如果以上条件都不满足，返回 -1，表示不执行这个技能槽
    }

    public void Build(Slot slot)// 构建技能槽的方法
    {
        slot.Add(SpellsDefine.PitchPerfect.GetSpell());// 添加 "Pitch Perfect" 技能到技能槽中
    }
}