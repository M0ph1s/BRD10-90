using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using static System.Formats.Asn1.AsnWriter;

namespace Morpheus;

public static class Helper
{
    public static CharacterAgent 自身 => Core.Me;

    public static CharacterAgent 自身目标 => Core.Me.GetCurrTarget();

    public static CharacterAgent 自身目标的目标 => Core.Me.GetCurrTargetsTarget();

    public static uint 自身血量 => Core.Me.CurrentHealth;

    public static uint 自身蓝量 => Core.Me.CurrentMana;

    public static float 自身血量百分比 => Core.Me.CurrentHealthPercent;

    public static float 自身蓝量百分比 => Core.Me.CurrentManaPercent;


    /// <summary>
    /// 自身存在Buff
    /// </summary>
    /// <param name="id">Buff id</param>
    /// <returns></returns>
    public static bool 自身存在Buff(uint id)
    {
        return Core.Me.HasAura(id);
    }
    public static bool 自身存在自身施加Buff(uint id)
    {
        return Core.Me.HasMyAura(id);
    }
    public static bool 敌人存在Buff(uint id)
    {
        return Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(id);
    }

    /// <summary>
    /// 自己身上的Buff剩余时间大于时间(ms)
    /// </summary>
    /// <param name="id">Buff id</param>
    /// <param name="time">时间(ms)</param>
    /// <returns></returns>
    public static bool 自身存在Buff大于时间(uint id, int time)
    {
        return Core.Me.HasMyAuraWithTimeleft(id, time);
    }
    public static bool 敌人存在Buff大于时间(uint id, int time)
    {
        return Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(id, time);
    }
    /// <summary>
    /// 技能是否可用,仅检查蓝量和技能是否解锁
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static bool 技能可用(uint id)
    {
        return Core.Get<IMemApiSpell>().IsReady(id);
    }

    /// <summary>
    /// 返回自身周围单位数量
    /// </summary>
    /// <param name="range">范围</param>
    /// <returns></returns>
    public static int 自身周围单位数量(int range)
    {
        return TargetHelper.GetNearbyEnemyCount(range);
    }

    /// <summary>
    /// 返回兽魂的数值
    /// </summary>
    /// <returns></returns>
   

    /// <summary>
    /// 自身当前目标是否在自身近战距离
    /// </summary>
    /// <returns></returns>
    public static bool 目标在自身近战距离()
    {
        return Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
               SettingMgr.GetSetting<GeneralSettings>().AttackRange;
    }

    /// <summary>
    /// 返回上一个连击的技能
    /// </summary>
    /// <returns></returns>
    public static uint 上一个连击技能()
    {
        return Core.Get<IMemApiSpell>().GetLastComboSpellId();
    }

    public static Spell 获取技能(this uint id)
    {
        return id.GetSpell();
    }

    /// <summary>
    /// 返回GCD的剩余时间(ms)
    /// </summary>
    /// <returns></returns>
    public static int GCD剩余时间()
    {
        return AI.Instance.GetGCDCooldown();
    }

    /// <summary>
    /// 技能是否刚使用过,默认1200ms
    /// </summary>
    /// <param name="spellId"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static bool 技能刚使用过(this uint spellId, int time = 1200)
    {
        return spellId.GetSpell().RecentlyUsed(time);
    }

    public static float 充能层数(this uint spellId)
    {
        return Core.Get<IMemApiSpell>().GetCharges(spellId);
    }


    public static bool 已解锁(uint spellId)
    {
        return Core.Get<IMemApiSpell>().IsLevelEnough(spellId);
    }

    public static uint 精通技能(uint spellId)
    {
        return Core.Get<IMemApiSpell>().CheckActionChange(spellId);
    }
    public static int 灵魂之声()
    {
        return Core.Get<IMemApiBard>().SoulVoice();
    }
    public static int 诗心()
    {
        return Core.Get<IMemApiBard>().Repertoire();
    }
    public static BardSong 当前歌曲()
    {
        return Core.Get<IMemApiBard>().ActiveSong();
    }
    public static int 尾声标识()
    {
        return Core.Get<IMemApiBard>().Coda();
    }
}
    