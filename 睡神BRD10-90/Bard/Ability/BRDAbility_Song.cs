using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Morpheus.Bard.Ability;

public class BRDAbility_Song : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;// 表示这个技能槽是属于 OffGCD（非公共冷却时间）的

    public int Check()// 检查是否需要施放歌曲技能的方法
    {
        if (Playlist.Song() != null) return 0;// 如果 BRDSpellHelper.Song() 返回的歌曲技能不为 null，返回 0，表示可以执行这个技能槽
        return -1;// 返回 -1，表示不执行这个技能槽
    }

    public void Build(Slot slot)// 构建技能槽的方法
    {
        slot.Add(Playlist.Song());// 添加 BRDSpellHelper.Song() 返回的歌曲技能到技能槽中
    }
}