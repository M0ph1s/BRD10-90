#region

using CombatRoutine;
using Common.Define;
using Morpheus.Bard;

#endregion

namespace Morpheus.Bard;

public class BRDRotationEventHandler : IRotationEventHandler//照抄，“WARRotationEventHandler”改成你想要的名字，请以aaaRotationEventHandle命名
{
    public void OnResetBattle()//照抄，代表了重新开始战斗的行为
    {
        BRDBattleData.Instance.Reset();//照抄，“WARBattleData”改成和你创建的BattleData名字一致
        Qt.Reset();//照抄
    }

    public Task OnNoTarget()//照抄，代表了没有目标时候的行为
    {
        return Task.CompletedTask;//照抄
    }

    public void AfterSpell(Slot slot, Spell spell)//照抄，代表了使用了技能后的行为
    {
        switch (spell.Id)//具体逻辑
        {
            
        }
    }

    public void OnBattleUpdate(int currTime)//照抄
    {
        //PLDSpellHelper.CheckOath();
    }

    public Task OnPreCombat()//照抄
    {
        return Task.CompletedTask;
    }
}