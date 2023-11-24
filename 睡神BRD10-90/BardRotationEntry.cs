#region
using CombatRoutine;
using CombatRoutine.Opener;
using CombatRoutine.View.JobView;
using Common;
using Common.Define;
using Common.Language;
using Morpheus.Bard;
using Morpheus.Bard.Ability;
using Morpheus.Bard.GCD;
#endregion

namespace Morpheus;

public class BardRotationEntry : IRotationEntry
{
    public static JobViewWindow JobViewWindow;//界面UI相关的，复制照抄
    private BardOverlay _lazyOverlay = new BardOverlay();
    /*界面UI相关的，照抄，“private WarriorOverlay _lazyOverlay = new WarriorOverlay();”里的“WarriorOverlay”字样可以自行定义*/




    public List<ISlotResolver> SlotResolvers = new()
    {
        new BRDGCD_IronJaws(),
        new BRDGCD_Dot1(),
        new BRDGCD_Dot2(),
        new BRDGCD_ApexArrow(),
        new BRDGCD_AOE2(),
        new BRDGCD_AOE1(),
        new BRDGCD_2(),
        new BRDGCD_1(),


        new BRDAbility_PitchPerfect(),
        new BRDAbility_Song(),
        new BRDAbility_RagingStrikes(),
        new BRDAbility_RadiantFinale(),
        new BRDAbility_BattleVoice(),
        new BRDAbility_EmpyrealArrow(),
        new BRDAbility_Barrage(),
        new BRDAbility_Sidewinder(),
        new BRDAbility_Bloodletter(),
        new BRDAbility_RainofDeath()

    };

    public string OverlayTitle { get; } = "Morpheus BRD";//你的标题，显示在ACR上的

    public void DrawOverlay()//照抄
    {
    }

    public string AuthorName { get; } = "Morpheus";//作者名称，Default替换成任何你想要的字符都行
    public Jobs TargetJob { get; } = Jobs.Bard;//设定该ACR使用的职业“public Jobs TargetJob { get; } = Jobs.Warrior;”里，你需要更改的是“Jobs.Warrior”这部分，Jobs.xxx，xxx填写职业

    public AcrType AcrType { get; } = AcrType.Normal;

    public Rotation Build(string settingFolder)//照抄
    {
        BRDSettings.Build(settingFolder);//照抄，“WARSettings”改成你自己写的
        return new Rotation(this, () => SlotResolvers)//照抄
            .SetRotationEventHandler(new BRDRotationEventHandler())//照抄，“.SetRotationEventHandler(new WARRotationEventHandler())”括号里的“new WARRotationEventHandler()”填写你自己的
            .AddSettingUIs(new PhyDpsBRDSettingView())//UI设置相关，照抄，括号里 的“new TankWARSettingView()”填写你自己的
            .AddSlotSequences(); //照抄


    }

    public bool BuildQt(out JobViewWindow jobViewWindow)//设置QT界面
    {
        jobViewWindow = new JobViewWindow(BRDSettings.Instance.JobViewSave, BRDSettings.Instance.Save, OverlayTitle);
        //照抄，括号内的格式为(AAA.Instance.JobViewSave,AAA.Instance.Save,OverlayTitle),AAA为你自己写的setting的名字
        JobViewWindow = jobViewWindow; // 这里设置一个静态变量.方便其他地方用，照抄
        jobViewWindow.AddTab("通用", _lazyOverlay.DrawGeneral);//QT里显示的通用那栏，照抄
        jobViewWindow.AddTab("时间轴", _lazyOverlay.DrawTimeLine);//QT里显示的时间轴那栏，照抄
        jobViewWindow.AddTab("DEV", _lazyOverlay.DrawDev);//QT里显示的DEV那栏，照抄

        /*QT开关写法：jobViewWindow.AddQt(String,bool),String改成你要写的信息用""引号引起来，bool填true或者false*/
        jobViewWindow.AddHotkey("LB", new HotKeyResolver_LB());//热键栏LB QT写法,照抄
        jobViewWindow.AddHotkey("疾跑", new HotkeyResolver_General(@"Resources/Spells/Sprint.png",
() =>
{
_ = AI.Instance.BattleData.NextSlot.Add(new Spell(SpellsDefine.Sprint, Core.Me));
}));/*通用技能QT写法：new HotkeyResolver_General(Image,Action),其中Image为你的技能图标位置，Action为具体你要编写的逻辑
     Action格式：() =>{ 具体逻辑 }*/


        return true;
    }
    private IOpener opener90 = new OpenerBRD90();

    private IOpener? GetOpener(uint level)
    {
        if (level < 90) return null;
        return opener90;
    }


    public void OnLanguageChanged(LanguageType languageType)//切换语言相关，不懂就照抄
    {
    }

}