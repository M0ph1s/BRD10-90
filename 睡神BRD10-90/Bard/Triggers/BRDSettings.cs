#region

using System.Numerics;
using CombatRoutine.View.JobView;
using Common.Helper;

#endregion

namespace Morpheus.Bard;

public class BRDSettings//你的ACR设置的名称
{
    public static BRDSettings Instance;//照抄，“WARSettings”改成和上头class那块一样的
    private static string path;//照抄
    public bool AutoReset = true;

    public JobViewSave JobViewSave = new() { MainColor = new Vector4(168 / 255f, 20 / 255f, 20 / 255f, 0.8f) };//设置了QT界面的颜色

    public Dictionary<string, object> StyleSetting = new();//照抄

    public int Time = 100;
    public bool TP = false;

    public static void Build(string settingPath)//照抄
    {
        path = Path.Combine(settingPath, "BRDSettings.json");//照抄，括号里面的“"WARSettings.json"”的“WARSettings.json”格式是xxx.json，请和你class保持一致
        if (!File.Exists(path))//照抄
        {
            Instance = new BRDSettings();////照抄，“WARSettings()”改成和你class一样的名字
            Instance.Save();//照抄
            return;//照抄
        }

        try//照抄
        {
            Instance = JsonHelper.FromJson<BRDSettings>(File.ReadAllText(path));//照抄，“WARSettings”保持和class一致
        }
        catch (Exception e)//照抄
        {
            Instance = new BRDSettings();//照抄，“WARSettings”保持和class一致
            LogHelper.Error(e.ToString());//照抄
        }
    }

    public void Save()//照抄
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path));//照抄
        File.WriteAllText(path, JsonHelper.ToJson(this));//照抄
    }
}