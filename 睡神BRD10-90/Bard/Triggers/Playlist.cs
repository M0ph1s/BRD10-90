using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Morpheus.Bard;

public static class Playlist
{


    public static bool IsRagingStrikes()
    {
        if (Helper.自身存在Buff(Buff.猛者强击))// 判断是否处于猛者强击状态
        {
            if (!Helper.自身存在Buff大于时间(Buff.猛者强击, 3000)) return true;

            return false;
        }

        return false;// 如果拥有猛者强击Buff，但剩余时间小于3秒，返回true，否则返回false
    }
    public static bool IsPitchPerfect()// 判断是否可以使用完美音调技能
    {
        if (Helper.已解锁(歌.放浪神的小步舞曲)) return true;

        var num = Core.Get<IMemApiBard>().SongTimer().TotalSeconds - 1;
        if (num % 3 == 0) return true;
        return false;// 获取当前演奏的时间，如果是3的倍数，返回true，否则返回false
    }


    public static Spell Song()// 获取当前演奏的歌曲
    {
        if (Helper.当前歌曲() == BardSong.None)
            if (Helper.已解锁(歌.贤者的叙事谣))
                if (!Helper.已解锁(歌.放浪神的小步舞曲))
                    if ((歌.贤者的叙事谣.获取技能().Cooldown.TotalSeconds < 1))
                        return 歌.贤者的叙事谣.获取技能();

        if (Helper.当前歌曲() == BardSong.None)
            if (Helper.已解锁(歌.放浪神的小步舞曲))
                if (!(歌.放浪神的小步舞曲.获取技能().Cooldown.TotalSeconds < 10))
                    if (歌.贤者的叙事谣.获取技能().Cooldown.TotalSeconds < 1)
                        return 歌.贤者的叙事谣.获取技能();



        if (Helper.当前歌曲() == BardSong.None)
            if (Helper.已解锁(歌.放浪神的小步舞曲))
                if (Helper.技能可用(歌.放浪神的小步舞曲))
                    if ((歌.放浪神的小步舞曲.获取技能().Cooldown.TotalSeconds < 1))
                        return 歌.放浪神的小步舞曲.获取技能();

        if (Helper.当前歌曲() == BardSong.None)
            if (Helper.已解锁(歌.军神的赞美歌))
                if (!(歌.贤者的叙事谣.获取技能().Cooldown.TotalSeconds < 10))
                    if (歌.军神的赞美歌.获取技能().Cooldown.TotalSeconds < 1)
                        return 歌.军神的赞美歌.获取技能();


        if (Helper.当前歌曲() == BardSong.TheWanderersMinuet)//这行代码检查吟游诗人（Bard）当前演奏的歌曲是否是“放浪神的小步舞曲”。
            if (Core.Get<IMemApiBard>().SongTimer().TotalMilliseconds <= 2000)
                return 歌.贤者的叙事谣.获取技能();// 如果当前演奏的歌曲是“放浪神的小步舞曲”，并且演奏时间小于等于2000毫秒，返回“贤者的叙事摇”技能
        if (Helper.当前歌曲() == BardSong.MagesBallad)//这行代码检查吟游诗人（Bard）当前演奏的歌曲是否是“贤者的叙事摇”。
            if (Helper.已解锁(歌.军神的赞美歌))
                if (Core.Get<IMemApiBard>().SongTimer().TotalMilliseconds <= 11000)
                return 歌.军神的赞美歌.获取技能();// 如果当前演奏的歌曲是“贤者的叙事摇”，并且演奏时间小于等于11000毫秒，返回 "军神的赞美歌" 技能
        if (Helper.当前歌曲() == BardSong.ArmysPaeon)//这行代码检查吟游诗人（Bard）当前演奏的歌曲是否是“军神的赞美歌”。
            if (Helper.已解锁(歌.放浪神的小步舞曲))
                if (Core.Get<IMemApiBard>().SongTimer().TotalMilliseconds <= 2000)
                return 歌.放浪神的小步舞曲.获取技能();// 如果当前演奏的歌曲是 "军神的赞美歌"，并且演奏时间小于等于2000毫秒，返回 放浪神的小步舞曲" 技能
        return null;// 如果以上条件都不满足，返回 null

    }
}