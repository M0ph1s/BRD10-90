using CombatRoutine;
using CombatRoutine.Opener;
using Common;
using Common.Define;
using Common.Helper;

namespace Morpheus.Bard;

public class OpenerBRD90 : IOpener
{
    public int StartCheck()
    {
        if (PartyHelper.NumMembers <= 4 && !Core.Me.GetCurrTarget().IsDummy())
            return -100;// 如果当前队伍成员数量小于等于4并且目标不是虚拟目标（非虚拟敌对单位），返回 -100
        if (!SpellsDefine.Barrage.IsReady())
            return -4;// 如果 "纷乱箭" 技能不处于可用状态，返回 -4
        return 0;// 如果以上条件都不满足，返回 0，表示可以执行动作
    }

    private static void Step0(Slot slot)
    {
        slot.Add(new Spell(技能.狂风蚀箭, SpellTargetType.Target));// 向 slot 中添加 "Stormbite" 技能，以当前目标为目标
        slot.Add(new SlotAction(SlotAction.WaitType.WaitForSndHalfWindow, -400,
            new Spell(歌.放浪神的小步舞曲, SpellTargetType.Target)));// 向 slot 中添加一个等待动作，等待半个窗口的时间，然后执行 "The Wanderer's Minuet" 技能
        slot.Add(new Spell(技能.猛者强击, SpellTargetType.Self));// 向 slot 中添加 "Raging Strikes" 技能，以自身为目标
    }

    private static void Step1(Slot slot)
    {
        slot.Add(new Spell(技能.烈毒咬箭, SpellTargetType.Target));// 向 slot 中添加 "Caustic Bite" 技能，以当前目标为目标
        slot.Add(new Spell(技能.九天连箭, SpellTargetType.Target));// 向 slot 中添加 "Empyreal Arrow" 技能，以当前目标为目标
        slot.Add(new Spell(技能.失血箭, SpellTargetType.Target));// 向 slot 中添加 "Bloodletter" 技能，以当前目标为目标
        slot.Wait2NextGcd = true;// 设置 slot 的 Wait2NextGcd 属性为 true，可能表示等待到下一个全局冷却 (GCD) 执行
    }

    private static void Step2(Slot slot)
    {
        if (Helper.自身存在Buff(Buff.直线射击预备))// 如果当前角色拥有 "Straighter Shot" 光辉射击的光环
            slot.Add(new Spell(技能.辉煌箭, SpellTargetType.Target));// 向 slot 中添加 "Straight Shot" 技能，使用当前目标为技能目标
        else
            slot.Add(new Spell(技能.爆发射击, SpellTargetType.Target));// 向 slot 中添加 "Heavy Shot" 技能，使用当前目标为技能目标
        slot.Add(new Spell(技能.光明神的最终乐章, SpellTargetType.Self));// 向 slot 中添加 "Radiant Finale" 技能，以自身为目标
        slot.Add(new Spell(技能.战斗之声, SpellTargetType.Self));// 向 slot 中添加 "Battle Voice" 技能，以自身为目标
        slot.Wait2NextGcd = true;// 设置 slot 的 Wait2NextGcd 属性为 true，可能表示等待到下一个全局冷却 (GCD) 执行
    }

    private static void Step3(Slot slot)
    {
        if (Helper.自身存在Buff(Buff.直线射击预备))// 如果当前角色拥有 "Straighter Shot" 光辉射击的光环
        {
            slot.Add(new Spell(技能.辉煌箭, SpellTargetType.Target));// 向 slot 中添加 "Straight Shot" 技能，使用当前目标为技能目标
            if (Helper.诗心() == 3)// 如果当前 "Repertoire" 堆叠数为 3
            {
                slot.Add(new Spell(技能.完美音调, SpellTargetType.Target));// 向 slot 中添加 "Pitch Perfect" 技能，使用当前目标为技能目标
                slot.Add(new Spell(SpellsDefine.Barrage, SpellTargetType.Self));// 向 slot 中添加 "Barrage" 技能，以自身为目标
            }
            else
            {
                slot.Add(new Spell(SpellsDefine.Barrage, SpellTargetType.Self));// 向 slot 中添加 "Barrage" 技能，以自身为目标
                slot.Add(new Spell(SpellsDefine.Sidewinder, SpellTargetType.Target));// 向 slot 中添加 "Sidewinder" 技能，使用当前目标为技能目标
            }
        }
        else
        {
            slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.HeavyShot.GetSpell().Id).GetSpell());// 向 slot 中添加 "Heavy Shot" 技能，使用当前目标为技能目标
            if (Core.Get<IMemApiBard>().Repertoire() == 3)// 如果当前 "Repertoire" 堆叠数为 3
            {
                slot.Add(new Spell(SpellsDefine.PitchPerfect, SpellTargetType.Target));// 向 slot 中添加 "Pitch Perfect" 技能，使用当前目标为技能目标
                if (Core.Me.HasAura(AurasDefine.StraighterShot))// 如果当前角色拥有 "Straighter Shot" 光辉射击的光环
                    slot.Add(new Spell(SpellsDefine.Sidewinder, SpellTargetType.Target)); // 向 slot 中添加 "Sidewinder" 技能，使用当前目标为技能目标
                else
                    slot.Add(new Spell(SpellsDefine.Barrage, SpellTargetType.Self));// 向 slot 中添加 "Barrage" 技能，以自身为目标
            }
            else
            {
                if (Core.Me.HasAura(AurasDefine.StraighterShot))// 如果当前角色拥有 "Straighter Shot" 光辉射击的光环
                {
                    slot.Add(new Spell(SpellsDefine.Sidewinder, SpellTargetType.Target));// 向 slot 中添加 "Sidewinder" 技能，使用当前目标为技能目标
                    slot.Add(new Spell(SpellsDefine.Bloodletter, SpellTargetType.Target));// 向 slot 中添加 "Bloodletter" 技能，使用当前目标为技能目标
                }
                else
                {
                    slot.Add(new Spell(SpellsDefine.Barrage, SpellTargetType.Self));// 向 slot 中添加 "Barrage" 技能，以自身为目标
                    slot.Add(new Spell(SpellsDefine.Sidewinder, SpellTargetType.Target));// 向 slot 中添加 "Sidewinder" 技能，使用当前目标为技能目标
                }
            }
        }

        slot.Wait2NextGcd = true;// 设置 slot 的 Wait2NextGcd 属性为 true，可能表示等待到下一个全局冷却 (GCD) 执行
    }

    private static void Step4(Slot slot)
    {
        if (Core.Me.HasAura(AurasDefine.StraighterShot))// 如果当前角色拥有 "Straighter Shot" 光辉射击的光环
        {
            slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.StraightShot.GetSpell().Id).GetSpell());// 向 slot 中添加 "Straight Shot" 技能，使用当前目标为技能目标
            if (Core.Get<IMemApiBard>().Repertoire() == 3)// 如果当前 "Repertoire" 堆叠数为 3
            {
                slot.Add(new Spell(SpellsDefine.PitchPerfect, SpellTargetType.Target));// 向 slot 中添加 "Pitch Perfect" 技能，使用当前目标为技能目标
                if (SpellsDefine.Barrage.IsReady())// 如果 "Barrage" 技能准备好
                {
                    slot.Add(new Spell(SpellsDefine.Barrage, SpellTargetType.Target));// 向 slot 中添加 "Barrage" 技能，使用当前目标为技能目标
                }
                else
                {
                    if (SpellsDefine.Sidewinder.IsReady())// 如果 "Sidewinder" 技能准备好
                        slot.Add(new Spell(SpellsDefine.Sidewinder, SpellTargetType.Target));// 向 slot 中添加 "Sidewinder" 技能，使用当前目标为技能目标
                }
            }
            else
            {
                if (SpellsDefine.Barrage.IsReady())// 如果 "Barrage" 技能准备好
                {
                    slot.Add(new Spell(SpellsDefine.Barrage, SpellTargetType.Target));// 向 slot 中添加 "Barrage" 技能，使用当前目标为技能目标
                    if (SpellsDefine.Sidewinder.IsReady())// 如果 "Sidewinder" 技能准备好
                        slot.Add(new Spell(SpellsDefine.Sidewinder, SpellTargetType.Target));// 向 slot 中添加 "Sidewinder" 技能，使用当前目标为技能目标
                    else
                        slot.Add(new Spell(SpellsDefine.Bloodletter, SpellTargetType.Target));// 向 slot 中添加 "Bloodletter" 技能，使用当前目标为技能目标
                }
                else
                {
                    if (SpellsDefine.Sidewinder.IsReady())// 如果 "Sidewinder" 技能准备好
                    {
                        slot.Add(new Spell(SpellsDefine.Sidewinder, SpellTargetType.Target));// 向 slot 中添加 "Sidewinder" 技能，使用当前目标为技能目标
                        slot.Add(new Spell(SpellsDefine.Bloodletter, SpellTargetType.Target));// 向 slot 中添加 "Bloodletter" 技能，使用当前目标为技能目标
                    }
                }
            }
        }
        else
        {
            slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.HeavyShot.GetSpell().Id).GetSpell());// 向 slot 中添加 "Heavy Shot" 技能，使用当前目标为技能目标
            if (Core.Get<IMemApiBard>().Repertoire() == 3)// 如果当前 "Repertoire" 堆叠数为 3
            {
                slot.Add(new Spell(SpellsDefine.PitchPerfect, SpellTargetType.Target));// 向 slot 中添加 "Pitch Perfect" 技能，使用当前目标为技能目标
                if (SpellsDefine.Barrage.IsReady() && !Core.Me.HasAura(AurasDefine.StraighterShot))// 如果 "Barrage" 技能准备好，并且当前角色没有 "Straighter Shot" 光辉射击的光环
                {
                    slot.Add(new Spell(SpellsDefine.Barrage, SpellTargetType.Target));// 向 slot 中添加 "Barrage" 技能，使用当前目标为技能目标
                }
                else
                {
                    if (SpellsDefine.Sidewinder.IsReady())// 如果 "Sidewinder" 技能准备好
                    {
                        slot.Add(new Spell(SpellsDefine.Sidewinder, SpellTargetType.Target));// 向 slot 中添加 "Sidewinder" 技能，使用当前目标为技能目标
                    }
                    else
                    {
                        if (SpellsDefine.Bloodletter.IsReady())// 如果 "Bloodletter" 技能准备好
                            slot.Add(new Spell(SpellsDefine.Bloodletter, SpellTargetType.Target));// 向 slot 中添加 "Bloodletter" 技能，使用当前目标为技能目标
                    }
                }
            }
            else
            {
                if (SpellsDefine.Barrage.IsReady() && !Core.Me.HasAura(AurasDefine.StraighterShot))// 如果 "Barrage" 技能准备好，并且当前角色没有 "Straighter Shot" 光辉射击的光环
                {
                    slot.Add(new Spell(SpellsDefine.Barrage, SpellTargetType.Target));// 向 slot 中添加 "Barrage" 技能，使用当前目标为技能目标
                    if (SpellsDefine.Sidewinder.IsReady())// 如果 "Sidewinder" 技能准备好
                        slot.Add(new Spell(SpellsDefine.Sidewinder, SpellTargetType.Target));// 向 slot 中添加 "Sidewinder" 技能，使用当前目标为技能目标
                    else
                        slot.Add(new Spell(SpellsDefine.Bloodletter, SpellTargetType.Target));// 向 slot 中添加 "Bloodletter" 技能，使用当前目标为技能目标
                }
                else
                {
                    if (SpellsDefine.Sidewinder.IsReady())// 如果 "Sidewinder" 技能准备好
                    {
                        slot.Add(new Spell(SpellsDefine.Sidewinder, SpellTargetType.Target));// 向 slot 中添加 "Sidewinder" 技能，使用当前目标为技能目标
                        slot.Add(new Spell(SpellsDefine.Bloodletter, SpellTargetType.Target));// 向 slot 中添加 "Bloodletter" 技能，使用当前目标为技能目标
                    }
                }
            }
        }

        slot.Wait2NextGcd = true;// 设置 slot 的 Wait2NextGcd 属性为 true，可能表示等待到下一个全局冷却 (GCD) 执行
    }

    private static void Step5(Slot slot)
    {
        if (Core.Me.HasAura(AurasDefine.StraighterShot))// 如果当前角色拥有 "Straighter Shot" 光辉射击的光环
        {
            slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.StraightShot.GetSpell().Id).GetSpell());// 向 slot 中添加 "Straight Shot" 技能，使用当前目标为技能目标
            if (Core.Get<IMemApiBard>().Repertoire() == 3)
                slot.Add(new Spell(SpellsDefine.PitchPerfect, SpellTargetType.Target));// 如果当前 "Repertoire" 堆叠数为 3，向 slot 中添加 "Pitch Perfect" 技能，使用当前目标为技能目标
            if (SpellsDefine.Bloodletter.IsReady())
                slot.Add(new Spell(SpellsDefine.Bloodletter, SpellTargetType.Target));// 如果 "Bloodletter" 技能准备好，向 slot 中添加 "Bloodletter" 技能，使用当前目标为技能目标
        }
        else
        {
            slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.HeavyShot.GetSpell().Id).GetSpell());// 向 slot 中添加 "Heavy Shot" 技能，使用当前目标为技能目标
            if (Core.Get<IMemApiBard>().Repertoire() == 3)
                slot.Add(new Spell(SpellsDefine.PitchPerfect, SpellTargetType.Target));// 如果当前 "Repertoire" 堆叠数为 3，向 slot 中添加 "Pitch Perfect" 技能，使用当前目标为技能目标
            if (SpellsDefine.Bloodletter.IsReady())
                slot.Add(new Spell(SpellsDefine.Bloodletter, SpellTargetType.Target));//如果 "Bloodletter" 技能准备好，向 slot 中添加 "Bloodletter" 技能，使用当前目标为技能目标
        }

        slot.Wait2NextGcd = true;// 设置 slot 的 Wait2NextGcd 属性为 true，可能表示等待到下一个全局冷却 (GCD) 执行
    }

    private static void Step6(Slot slot)
    {
        if (Core.Me.HasAura(AurasDefine.StraighterShot))// 如果当前角色拥有 "Straighter Shot" 光辉射击的光环
        {
            slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.StraightShot.GetSpell().Id).GetSpell());// 向 slot 中添加 "Straight Shot" 技能，使用当前目标为技能目标
            if (Core.Get<IMemApiBard>().Repertoire() == 3)
                slot.Add(new Spell(SpellsDefine.PitchPerfect, SpellTargetType.Target));// 如果当前 "Repertoire" 堆叠数为 3，向 slot 中添加 "Pitch Perfect" 技能，使用当前目标为技能目标
            if (SpellsDefine.Bloodletter.IsReady())
                slot.Add(new Spell(SpellsDefine.Bloodletter, SpellTargetType.Target));// 如果 "Bloodletter" 技能准备好，向 slot 中添加 "Bloodletter" 技能，使用当前目标为技能目标
        }
        else
        {
            slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.HeavyShot.GetSpell().Id).GetSpell());// 向 slot 中添加 "Heavy Shot" 技能，使用当前目标为技能目标
            if (Core.Get<IMemApiBard>().Repertoire() == 3)
                slot.Add(new Spell(SpellsDefine.PitchPerfect, SpellTargetType.Target));// 如果当前 "Repertoire" 堆叠数为 3，向 slot 中添加 "Pitch Perfect" 技能，使用当前目标为技能目标
            if (SpellsDefine.Bloodletter.IsReady())
                slot.Add(new Spell(SpellsDefine.Bloodletter, SpellTargetType.Target));// 如果 "Bloodletter" 技能准备好，向 slot 中添加 "Bloodletter" 技能，使用当前目标为技能目标
        }

        slot.Wait2NextGcd = true;// 设置 slot 的 Wait2NextGcd 属性为 true，可能表示等待到下一个全局冷却 (GCD) 执行
    }

    private static void Step7(Slot slot)
    {
        if (Core.Me.HasAura(AurasDefine.StraighterShot))// 如果当前角色拥有 "Straighter Shot" 光辉射击的光环
        {
            slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.StraightShot.GetSpell().Id).GetSpell());// 向 slot 中添加 "Straight Shot" 技能，使用当前目标为技能目标
            slot.Add(new Spell(SpellsDefine.EmpyrealArrow, SpellTargetType.Target));// 向 slot 中添加 "Empyreal Arrow" 技能，使用当前目标为技能目标
            if (Core.Get<IMemApiBard>().Repertoire() == 3)
            {
                slot.Add(new Spell(SpellsDefine.PitchPerfect, SpellTargetType.Target));// 如果当前 "Repertoire" 堆叠数为 3，向 slot 中添加 "Pitch Perfect" 技能，使用当前目标为技能目标
            }
            else
            {
                if (SpellsDefine.Bloodletter.IsReady())
                    slot.Add(new Spell(SpellsDefine.Bloodletter, SpellTargetType.Target));// 如果 "Bloodletter" 技能准备好，向 slot 中添加 "Bloodletter" 技能，使用当前目标为技能目标
            }
        }
        else
        {
            slot.Add(new Spell(SpellsDefine.IronJaws, SpellTargetType.Target));// 向 slot 中添加 "Iron Jaws" 技能，使用当前目标为技能目标
            slot.Add(new Spell(SpellsDefine.EmpyrealArrow, SpellTargetType.Target));// 向 slot 中添加 "Empyreal Arrow" 技能，使用当前目标为技能目标
            if (Core.Get<IMemApiBard>().Repertoire() == 3)
            {
                slot.Add(new Spell(SpellsDefine.PitchPerfect, SpellTargetType.Target));// 如果当前 "Repertoire" 堆叠数为 3，向 slot 中添加 "Pitch Perfect" 技能，使用当前目标为技能目标
            }
            else
            {
                if (SpellsDefine.Bloodletter.IsReady())
                    slot.Add(new Spell(SpellsDefine.Bloodletter, SpellTargetType.Target));// 如果 "Bloodletter" 技能准备好，向 slot 中添加 "Bloodletter" 技能，使用当前目标为技能目标
            }
        }

        slot.Wait2NextGcd = true;// 设置 slot 的 Wait2NextGcd 属性为 true，可能表示等待到下一个全局冷却 (GCD) 执行
    }

    private static void Step8(Slot slot)
    {
        if (Core.Me.HasAura(AurasDefine.StraighterShot))// 如果当前角色拥有 "Straighter Shot" 光辉射击的光环
        {
            slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.StraightShot.GetSpell().Id).GetSpell());// 向 slot 中添加 "Straight Shot" 技能，使用当前目标为技能目标
        }
        else
        {
            if (SpellsDefine.IronJaws.GetSpell().RecentlyUsed(5000))// 如果 "Iron Jaws" 技能在最近 5000 毫秒内被使用过
                slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.HeavyShot.GetSpell().Id).GetSpell());// 向 slot 中添加 "Heavy Shot" 技能，使用当前目标为技能目标
            else
                slot.Add(new Spell(SpellsDefine.IronJaws, SpellTargetType.Target));// 向 slot 中添加 "Iron Jaws" 技能，使用当前目标为技能目标
        }
        if (Core.Get<IMemApiBard>().Repertoire() > 0)
            slot.Add(new Spell(SpellsDefine.PitchPerfect, SpellTargetType.Target));// 如果当前 "Repertoire" 堆叠数大于 0，向 slot 中添加 "Pitch Perfect" 技能，使用当前目标为技能目标
        if (SpellsDefine.Bloodletter.IsReady()) slot.Add(new Spell(SpellsDefine.Bloodletter, SpellTargetType.Target));// 如果 "Bloodletter" 技能准备好，向 slot 中添加 "Bloodletter" 技能，使用当前目标为技能目标
    }

    public int StopCheck(int index)
    {
        return -1;//StopCheck 方法返回 -1，表示停手条件不满足，不需要停手。
    }

    public List<Action<Slot>> Sequence { get; } = new()
    {
        Step0,
        Step1,
        Step2,
        Step3,
        Step4,
        Step5,
        Step6,
        Step7,
        Step8
    };

    public Action CompeltedAction { get; set; }
    public uint Level { get; } = 90;

    public void InitCountDown(CountDownHandler countDownHandler)
    {
        //countDownHandler.AddAction(100,()=>DNCSpellHelper.BattleStart());
    }
}